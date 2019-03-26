using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PmsEteck.Data.Services
{
    public class CostlierService
    {
        public decimal TotalConsumption { get; private set; }
        public decimal LossOfDistribution { get; private set; }
        public decimal PollTax { get; private set; }
        public decimal PollTaxPerAddress { get { return PollTax / TotalAddresses; } }
        public decimal TotalCostlier { get; private set; }
        public decimal AverageCostlier { get; private set; }
        public ServiceRun ServiceRun { get; private set; }
        public int TotalAddresses => PurchaseAddresses.Count();

        PmsEteckContext PmsEteckContext = new PmsEteckContext();
        IEnumerable<Address> PurchaseAddresses;
        DateTime ConsumptionStartDate;
        DateTime ConsumptionEndDate;
        private Dictionary<int, decimal> AddressConsumption = new Dictionary<int, decimal>();

        public CostlierService(decimal consumption, DateTime startDate, DateTime endDate, Address salesAddress, ServiceRun serviceRun = null)
        {
            //Stap 1
            ServiceRun = serviceRun ?? new ServiceRun
            {
                dtServiceRunStartDate = DateTime.UtcNow,
                iServiceRunRowsUpdated = 0,
                ServiceRunErrors = new List<ServiceRunError>(),
                iServiceKey = 21
            };

            //Stap 2
            TotalConsumption = consumption;
            LossOfDistribution = Math.Min(salesAddress.MaxLossOfDistribution, decimal.Multiply(TotalConsumption, salesAddress.PercentLossOfDistribution));
            PollTax = Math.Min(salesAddress.MaxPollTax, decimal.Multiply((TotalConsumption - LossOfDistribution), salesAddress.PercentPollTax));
            ConsumptionStartDate = startDate;
            ConsumptionEndDate = endDate;

            //Stap 3: Select all purchaseAddresses under the CollectiveAddress
            PurchaseAddresses = PmsEteckContext.Addresses.Where(w => w.CollectiveAddressID == salesAddress.iAddressKey && w.sConnectionTypeKey.StartsWith("2")).ToList();

            //Stap 4 Bereken per verkoopaansluiting het totale deel van de kosten
            foreach (Address purchaseAddress in PurchaseAddresses)
            {
                CalculateConsumptionForAddress(purchaseAddress.iAddressKey);
            }
            TotalCostlier = AddressConsumption.Sum(sm => sm.Value);

            //Stap 5 Bereken het gemiddelde verbruk per aansluiting
            AverageCostlier = TotalCostlier == 0 ? 0 : TotalCostlier / AddressConsumption.Where(w => w.Value != 0).Count();

            foreach (KeyValuePair<int, decimal> addresConsumption in AddressConsumption)
            {
                //Stap 6 Zoek voor het adres de verkoopmeter op en de laats bekende stand op dit adres
                Address purchaseAddress = PurchaseAddresses.First(f => f.iAddressKey == addresConsumption.Key);
                Counter purchaseCounter = GetPurchaseCounter(purchaseAddress.iAddressKey);
                if (purchaseCounter != null)
                {
                    //Stap 7 Controle of er geen verbruik op de periode al bekend is
                    if (purchaseCounter.Consumption.All(u => u.dtEndDateTime <= ConsumptionStartDate))
                    {
                        //Stap 8 verbruik toevoegen aan het telwerk
                        // All consumption are below ConsumptionStartdate
                        // Get LastConsumptionRow
                        Consumption lastConsumption = purchaseCounter.GetLastConsumptionRow();
                        if (lastConsumption.dtEndDateTime == ConsumptionStartDate)
                        {
                            //ConsumptionStartdate is followup the last consumptionrow
                            //Create new consumptionrow
                            Consumption purchaseConsumption = new Consumption
                            {
                                bExcludeForReport = false,
                                bValidated = addresConsumption.Value != 0,
                                dConsumption = (TotalConsumption - PollTax - LossOfDistribution) / TotalCostlier * (addresConsumption.Value == 0 ? AverageCostlier : addresConsumption.Value),
                                dEndPosition = lastConsumption.dEndPosition + (TotalConsumption - PollTax - LossOfDistribution) / TotalCostlier * (addresConsumption.Value == 0 ? AverageCostlier : addresConsumption.Value),
                                dtEndDateTime = ConsumptionEndDate,
                                dtStartDateTime = lastConsumption.dtEndDateTime,
                                iAddressKey = addresConsumption.Key,
                                ServiceRun = ServiceRun
                            };
                            //Add consumptionrow to counter
                            purchaseCounter.Consumption.Add(purchaseConsumption);
                            PmsEteckContext.Entry(purchaseCounter).State = EntityState.Modified;
                            //Create new log for adding consumption
                            ServiceRun.iServiceRunRowsUpdated++;
                            ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                            {
                                dtEffectiveDateTime = ConsumptionEndDate.ToUniversalTime(),
                                iStatusCode = 200,
                                sConsumptionMeterNumber = purchaseCounter.ConsumptionMeter.sConsumptionMeterNumber,
                                sErrorMessage = string.Format("Er is een verbruik van {0} GJ toegevoegd aan telwerk {1}", purchaseConsumption.dConsumption.ToString("N2"), purchaseCounter.sCounterCode)
                            });
                        }
                        else
                        {
                            ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                            {
                                dtEffectiveDateTime = ConsumptionStartDate.ToUniversalTime(),
                                iStatusCode = (int)HttpStatusCode.BadRequest,
                                sConsumptionMeterNumber = purchaseCounter.ConsumptionMeter.sConsumptionMeterNumber,
                                sErrorMessage = string.Format("De datum van de laatst bekende stand ({0}) is niet gelijk aan {1}.", lastConsumption.dtEndDateTime.ToShortDateString(), ConsumptionStartDate.ToShortDateString())
                            });
                        }
                    }
                    else
                    {
                        ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                        {
                            dtEffectiveDateTime = ConsumptionStartDate.ToUniversalTime(),
                            iStatusCode = (int)HttpStatusCode.BadRequest,
                            sConsumptionMeterNumber = purchaseCounter.ConsumptionMeter.sConsumptionMeterNumber,
                            sErrorMessage = string.Format("Er zijn standen bekend op telwerk {0} die na {1} liggen.", purchaseCounter.sCounterCode, ConsumptionStartDate.ToShortDateString())
                        });

                    }
                }
                else
                {
                    ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        dtEffectiveDateTime = ConsumptionStartDate,
                        iStatusCode = (int)HttpStatusCode.NotFound,
                        sErrorMessage = string.Format("Bij adres {0} is geen verkoop - thermisch telwerk gevonden", purchaseAddress.GetAddressString())
                    });
                }

                //Stap 11 vind per adres het telwerk voor de hoofdelijke omslag
                Counter pollTaxCounter = GetPollTaxCounter(purchaseAddress.iAddressKey);
                if (pollTaxCounter != null)
                {
                    if (pollTaxCounter.Consumption.All(a => a.dtEndDateTime <= ConsumptionStartDate))
                    {
                        //Get LastConsumptionRow
                        Consumption lastConsumptionRow = pollTaxCounter.GetLastConsumptionRow();
                        if (lastConsumptionRow.dtEndDateTime == ConsumptionStartDate)
                        {
                            //Create new consumptionrow
                            Consumption purchaseConsumption = new Consumption
                            {
                                bExcludeForReport = false,
                                bValidated = addresConsumption.Value != 0,
                                dConsumption = PollTaxPerAddress,
                                dEndPosition = lastConsumptionRow.dEndPosition + PollTaxPerAddress,
                                dtEndDateTime = ConsumptionEndDate,
                                dtStartDateTime = lastConsumptionRow.dtEndDateTime,
                                iAddressKey = addresConsumption.Key,
                                ServiceRun = ServiceRun
                            };
                            //Add consumptionrow to counter
                            pollTaxCounter.Consumption.Add(purchaseConsumption);
                            PmsEteckContext.Entry(pollTaxCounter).State = EntityState.Modified;
                            //Create new log for adding consumption
                            ServiceRun.iServiceRunRowsUpdated++;
                            ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                            {
                                dtEffectiveDateTime = ConsumptionEndDate,
                                iStatusCode = 200,
                                sConsumptionMeterNumber = pollTaxCounter.ConsumptionMeter.sConsumptionMeterNumber,
                                sErrorMessage = string.Format("Er is een verbruik van {0} GJ toegevoegd aan telwerk {1}", purchaseConsumption.dConsumption.ToString("N2"), pollTaxCounter.sCounterCode)
                            });
                        }
                        else
                        {
                            ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                            {
                                dtEffectiveDateTime = ConsumptionStartDate.ToUniversalTime(),
                                iStatusCode = (int)HttpStatusCode.BadRequest,
                                sConsumptionMeterNumber = pollTaxCounter.ConsumptionMeter.sConsumptionMeterNumber,
                                sErrorMessage = string.Format("De datum van de laatst bekende stand ({0}) is niet gelijk aan {1}.", lastConsumptionRow.dtEndDateTime.ToShortDateString(), ConsumptionStartDate.ToShortDateString())
                            });
                        }
                    }
                    else
                    {
                        ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                        {
                            dtEffectiveDateTime = ConsumptionStartDate.ToUniversalTime(),
                            iStatusCode = (int)HttpStatusCode.BadRequest,
                            sConsumptionMeterNumber = pollTaxCounter.ConsumptionMeter.sConsumptionMeterNumber,
                            sErrorMessage = string.Format("Er zijn standen bekend op telwerk {0} die na {1} liggen.", pollTaxCounter.sCounterCode, ConsumptionStartDate.ToShortDateString())
                        });

                    }
                }
                else
                {
                    ServiceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        dtEffectiveDateTime = ConsumptionStartDate.ToUniversalTime(),
                        iStatusCode = (int)HttpStatusCode.NotFound,
                        sErrorMessage = string.Format("Bij adres {0} is geen telwerk met telwerktype Warmte - 2 gevonden", purchaseAddress.GetAddressString())
                    });
                }
            }
            ServiceRun.iServiceRunStatus = ServiceRun.ServiceRunErrors.All(a => a.iStatusCode == 200) ? 200 : (int)HttpStatusCode.BadRequest;
            ServiceRun.dtServiceRunEndDate = DateTime.UtcNow;
            PmsEteckContext.ServiceRuns.Add(ServiceRun);
            PmsEteckContext.SaveChanges();
        }

        private Counter GetPollTaxCounter(int iAddressKey)
        {
            return PmsEteckContext.Counters.Include(i => i.Consumption).FirstOrDefault(f => f.iCounterTypeKey == 17 && f.iUnitKey == 3 && f.ConsumptionMeter.iMeterTypeKey == 8 && f.ConsumptionMeter.iAddressKey == iAddressKey) ?? null;
        }

        private Counter GetPurchaseCounter(int iAddressKey)
        {
            return PmsEteckContext.Counters.Include(i => i.Consumption).FirstOrDefault(f => f.iCounterTypeKey == 5 && f.iUnitKey == 3 && f.ConsumptionMeter.iMeterTypeKey == 8 && f.ConsumptionMeter.iAddressKey == iAddressKey) ?? null;
        }

        void CalculateConsumptionForAddress(int addressID)
        {
            decimal consumptionRow = PmsEteckContext.Consumption
                .Where(w => w.iAddressKey == addressID &&
                    w.dtStartDateTime == ConsumptionStartDate &&
                    w.dtEndDateTime == ConsumptionEndDate &&
                    w.Counter.iCounterTypeKey == 16 &&
                    w.Counter.ConsumptionMeter.iMeterTypeKey == 14).Count() != 0 ? PmsEteckContext.Consumption
                .Where(w => w.iAddressKey == addressID &&
                    w.dtStartDateTime == ConsumptionStartDate &&
                    w.dtEndDateTime == ConsumptionEndDate &&
                    w.Counter.iCounterTypeKey == 16 &&
                    w.Counter.ConsumptionMeter.iMeterTypeKey == 14)
                .Sum(sm => sm.dConsumption) : 0;
            AddressConsumption.Add(addressID, consumptionRow);
        }

        public decimal GetConsumption(int purchaseAddressID)
        {
            //Select purchaseAdress
            if (PurchaseAddresses.Any(u => u.iAddressKey == purchaseAddressID))
            {
                return PmsEteckContext.Consumption
                .Where(w => w.iAddressKey == purchaseAddressID &&
                    w.dtStartDateTime == ConsumptionStartDate &&
                    w.dtEndDateTime == ConsumptionEndDate &&
                    w.Counter.iCounterTypeKey == 16 &&
                    w.Counter.ConsumptionMeter.iMeterTypeKey == 14).Count() != 0 ? PmsEteckContext.Consumption
                .Where(w => w.iAddressKey == purchaseAddressID &&
                    w.dtStartDateTime == ConsumptionStartDate &&
                    w.dtEndDateTime == ConsumptionEndDate &&
                    w.Counter.iCounterTypeKey == 16 &&
                    w.Counter.ConsumptionMeter.iMeterTypeKey == 14)
                .Sum(sm => sm.dConsumption) : 0;
            }
            throw new ArgumentNullException(nameof(purchaseAddressID));
        }
    }
}
