using Brunata.Services;
using Esight.Services;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    public class ConsumptionMeterService
    {
        PmsEteckContext EteckContext;
        public List<Esight.Models.Meter> MeterList { get; private set; }
        public List<Consumption> ConsumptionList { get; private set; }
        public List<Esight.Models.Company> CompanyList { get; private set; }
        public List<Esight.Models.Site> SiteList { get; private set; }
        public Eisight.Meter.MeterInfo MeterInfo { get; private set; }
        public ConsumptionMeterService()
        {
            EteckContext = new PmsEteckContext();
        }
        public ConsumptionMeterService(PmsEteckContext context)
        {
            EteckContext = context;
        }

        public async Task<Result> GetCompaniesAsync()
        {
            CompanyService companyService = new CompanyService();
            await companyService.GetCompanies();
            CompanyList = new List<Esight.Models.Company>();
            CompanyList.AddRange(companyService.Companies);
            return Result.Success;
        }

        public async Task<Result> GetMetersAsync(int? siteID)
        {
            MeterServices meterServices = new MeterServices();
            await meterServices.GetMetersForSiteId(siteID);
            MeterList = new List<Esight.Models.Meter>();
            MeterList.AddRange(meterServices.Meters);
            return await Task.FromResult(Result.Success);
        }

        public async Task<Result> GetSitesAsync(int? companyID)
        {
            SiteServices siteServices = new SiteServices();
            await siteServices.GetSites(companyID);
            SiteList = new List<Esight.Models.Site>();
            SiteList.AddRange(siteServices.Sites);
            return await Task.FromResult(Result.Success);
        }

        public async Task<Result> GetMeterDetailsAsync(int meterID)
        {
            MeterServices meterServices = new MeterServices();
            MeterInfo = await meterServices.GetMeterDetails(meterID);
            return await Task.FromResult(Result.Success);
        }

        public async Task<ServiceRun> AddBrunataStandingsAsync(List<Counter> counters, ServiceRun serviceRun, PmsEteckContext context)
        {
            ConsumptionList = new List<Consumption>();
            foreach (IGrouping<int?, Counter> item in counters.GroupBy(gr => gr.ConsumptionMeter.Address.ObjectID))
            {
                if (item.Key.HasValue)
                {
                    BrunataManager manager = new BrunataManager(item.Key.Value);
                    foreach (Counter counter in item)
                    {
                        context.Entry(counter)
                            .Collection(c => c.Consumption)
                            .Query()
                            .OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition)
                            .Take(10);
                        if (counter.Consumption.Max(m => m.dtEndDateTime) < DateTime.Today)
                        {
                            manager.GetDailyReadingsForMeter(counter.ConsumptionMeter.sConsumptionMeterNumber, counter.Consumption.Max(m => m.dtEndDateTime), DateTime.Today);
                        }
                    }
                }
            }
            return await Task.FromResult(serviceRun);
        }

        public async Task<ServiceRun> AddConsumptionsAsync(List<Counter> counters, ServiceRun serviceRun)
        {
            ConsumptionList = new List<Consumption>();
            MeterServices meterServices = new MeterServices();
            foreach (Counter counter in counters)
            {
                EteckContext.Entry(counter).Collection(c => c.Consumption).Query().OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).Take(1).ToList();
                try
                {
                    List<Esight.Models.Consumption> esightConsumptionList = new List<Esight.Models.Consumption>();
                    switch (counter.iCounterTypeKey)
                    {
                        case 3:
                        case 4:
                            if (await meterServices.MeterHasContract(counter.ConsumptionMeter.iEsighMeterID.Value))
                            {
                                esightConsumptionList = await meterServices.GetLowHighConsumption(counter.ConsumptionMeter.iEsighMeterID.Value, counter.Consumption.OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).FirstOrDefault().dtEndDateTime);
                            }
                            else
                            {
                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                {
                                    iStatusCode = 500,
                                    sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                                    sErrorMessage = string.Format("Bij de meter is geen contract gekoppeld, waardoor geen uitsplitsing normaal- / piekverbruik gemaakt kan worden.")
                                });
                                throw new ArgumentNullException();
                            }
                            break;
                        default:
                            esightConsumptionList = await meterServices.GetConsumptionFromMeter(counter.ConsumptionMeter.iEsighMeterID.Value, counter.Consumption.OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).FirstOrDefault().dtEndDateTime);
                            break;
                    }

                    decimal lastEndposition = counter.Consumption.OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).FirstOrDefault().dEndPosition;
                    foreach (Esight.Models.Consumption r in esightConsumptionList)
                    {
                        counter.Consumption.Add(new Consumption
                        {
                            bExcludeForReport = counter.ConsumptionMeter.MeterType.sPurchaseOrSale.Contains("Distribution"),
                            bValidated = true,
                            Counter = counter,
                            dConsumption = counter.iCounterTypeKey == 3 ? r.NightConsumption.GetValueOrDefault() : r.DayConsumption,
                            dEndPosition = lastEndposition + (counter.iCounterTypeKey == 3 ? r.NightConsumption.GetValueOrDefault() : r.DayConsumption),
                            dtEndDateTime = r.DateTime.Date.AddDays(1),
                            dtStartDateTime = r.DateTime,
                            iAddressKey = counter.ConsumptionMeter.iAddressKey,
                            ServiceRun = serviceRun
                        });
                        lastEndposition += counter.iCounterTypeKey == 3 ? r.NightConsumption.GetValueOrDefault() : r.DayConsumption;
                    }
                    serviceRun.iServiceRunRowsUpdated += esightConsumptionList.Count;
                    serviceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        iStatusCode = 200,
                        sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                        sErrorMessage = string.Format("Bij telwerk {0} {1} toegevoegd.", counter.sCounterCode, esightConsumptionList.Count == 1 ? "is 1 verbruiksregel" : string.Format("zijn {0} verbruiksregels", esightConsumptionList.Count))
                    });
                    Console.WriteLine(string.Format("Succes: Telwerk {0} is bijgewerkt", counter.sCounterCode));
                }
                catch (Exception ex)
                {
                    serviceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        iStatusCode = 500,
                        sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                        sErrorMessage = ex.Message,
                    });
                    Console.WriteLine(string.Format("Error: Bijwerken van telwerk {0} met id {1} is foutgegaan", counter.sCounterCode, counter.iCounterKey));
                    Console.WriteLine(string.Format("Foutmelding: {0}", ex.Message));
                }
            }

            return await Task.FromResult(serviceRun);
        }

        public async Task<ServiceRun> RemoveConsumptionLastTwoWeeksAsync(List<Counter> counters, ServiceRun serviceRun, PmsEteckContext db)
        {
            DateTime TwoWeeksAgo = DateTime.Today.AddDays(-14);

            foreach (Counter counter in counters)
            {
                int removedRows = 0;
                EteckContext.Entry(counter).Collection(c => c.Consumption).Query().OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).Take(50).ToList();
                try
                {
                    //Select first consumptionrow from counter
                    Consumption firstConsumptionRow = counter.Consumption.OrderBy(o => o.dtEndDateTime).ThenBy(o => o.dEndPosition).FirstOrDefault();

                    // Get list to remove from counter en remove from database
                    // Hiero
                    // Controleren op anderre servicerun dan 16 of 19. Alles wat later is dan de meest recente uit 'consumptions' verwijderen.
                    List<Consumption> consumptions = counter.Consumption.Where(w => w.dtEndDateTime >= TwoWeeksAgo && w.dtEndDateTime > firstConsumptionRow.dtEndDateTime).OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).ToList();

                    if (consumptions.Any(u => !new int[] { 16, 19 }.Contains(u.ServiceRun.iServiceKey)))
                    {
                        int index = consumptions.IndexOf(consumptions.FirstOrDefault(f => f.ServiceRun.iServiceKey != 16 && f.ServiceRun.iServiceKey != 19));
                        consumptions = consumptions.Take(index).ToList();
                    }

                    removedRows = counter.Consumption.RemoveAll(c => consumptions.Equals(c));
                    db.Consumption.RemoveRange(consumptions);
                    serviceRun.iServiceRunRowsUpdated += consumptions.Count;
                    serviceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        iStatusCode = 200,
                        sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                        sErrorMessage = string.Format("Bij telwerk {0} {1} verwijderd.", counter.sCounterCode, consumptions.Count == 1 ? "is 1 verbruiksregel" : string.Format("zijn {0} verbruiksregels", consumptions.Count))
                    });
                }
                catch (Exception ex)
                {
                    serviceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        iStatusCode = 500,
                        sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                        sErrorMessage = ex.Message,
                    });
                }
            }

            return await Task.FromResult(serviceRun);
        }
    }
}
