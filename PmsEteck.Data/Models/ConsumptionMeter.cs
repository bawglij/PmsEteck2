using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using PmsEteck.Data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace PmsEteck.Data.Models
{
    [Table(name: "ConsumptionMeters", Schema = "meter")]
    public class ConsumptionMeter
    {
        IHttpContextAccessor _httpContextAccessor;
        public ConsumptionMeter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ConsumptionMeter()
        {
        }

        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        private Fudura _fuduraService = new Fudura();
        private SmartDataSolution smartDataSolution = new SmartDataSolution();
        #endregion
        #region Properties

        [Key]
        public int iConsumptionMeterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Meternummer")]
        public string sConsumptionMeterNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "EAN-nummer")]
        public string sEANCode { get; set; }

        [Display(Name = "Object")]
        public int? iAddressKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Metertype")]
        public int iMeterTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Leverancier")]
        public int iConsumptionMeterSupplierKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Ophaalfrequentie")]
        public int iFrequencyKey { get; set; }

        [Display(Name = "Extra toelichting")]
        [MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn.")]
        public string sConsumptionMeterComment { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Bouwjaar")]
        public int iBuildYear { get; set; }

        [Display(Name = "Servicerun")]
        public int? iServiceRunKey { get; set; }

        [Display(Name = "Servicerun")]
        public virtual ServiceRun ServiceRun { get; set; }

        [Display(Name = "Telwerken")]
        public List<Counter> Counters { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "# dagen marge")]
        [Range(0, 365, ErrorMessage = "{0} moet een waarde bevatten tussen {1} en {2}")]
        public int iDaysMargin { get; set; }

        [Display(Name = "Meter vernietigd")]
        public bool bMeterDeleted { get; set; }

        [Display(Name = "Meetverantwoordelijke")]
        public int? iMeasuringOfficerID { get; set; }

        [Display(Name = "Netbeheerder")]
        public int? iOperatorID { get; set; }

        [Display(Name = "Energieleverancier")]
        public int? iEnergySupplierID { get; set; }

        [Display(Name = "Meterijkingpool?")]
        public bool bMeterCalibrationPool { get; set; }

        [Display(Name = "Metercode eSight")]
        [StringLength(15, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sMeterCodeEsight { get; set; }

        [Display(Name = "Esigh MeterID")]
        public int? iEsighMeterID { get; set; }

        [Display(Name = "Metercode")]
        [StringLength(100, ErrorMessage = "{0} max maximaal {1} tekens bevatten")]
        public string sMeterPoolCode { get; set; }

        [Display(Name = "Zichtbaar voor klant")]
        public bool bVisibleForCustomers { get; set; }

        public virtual ConsumptionMeterSupplier Supplier { get; set; }

        public virtual MeterType MeterType { get; set; }

        public virtual Address Address { get; set; }

        public virtual Frequency Frequency { get; set; }

        public virtual Operator Operator { get; set; }

        public virtual EnergySupplier EnergySupplier { get; set; }

        public virtual MeasuringOfficer MeasuringOfficer { get; set; }

        public List<MeterChangeLog> MeterChangeLogs { get; set; }

        //public List<MaximumPower> MaximumPowers { get; set; }

        #endregion
        #region Methods
        public async Task<ServiceRun> GetMeterConsumption(int consumptionMeterKey, ServiceRun serviceRun)
        {
            ConsumptionMeter consumptionMeter = await db.ConsumptionMeters.Include(i => i.Counters).FirstOrDefaultAsync(f => f.iConsumptionMeterKey == consumptionMeterKey);
            if (consumptionMeter.Counters.Count == 0)
            {
                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                {
                    dtEffectiveDateTime = DateTime.UtcNow,
                    iStatusCode = (int)HttpStatusCode.NotFound,
                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                    sErrorMessage = string.Format("Er zijn nog geen telwerken gevonden voor de meter met EANCode {0}", consumptionMeter.sEANCode),
                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                });
                return serviceRun;
            }
            DateTime today = DateTime.Today;
            DateTime authorizedFromDate = _fuduraService.GetAuthorizedDateFrom(consumptionMeter.sEANCode);
            List<Consumption> consumptionList = new List<Consumption>();
            try
            {
                foreach (Counter counter in consumptionMeter.Counters.Where(w => !new[] { 11, 14, 15 }.Contains(w.iCounterTypeKey)))
                {
                    Consumption lastConsumption = db.Consumption.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dtEndDateTime).ThenByDescending(t => t.dEndPosition).FirstOrDefault();

                    if (lastConsumption == null)
                    {
                        serviceRun.ServiceRunErrors.Add(new ServiceRunError
                        {
                            dtEffectiveDateTime = DateTime.UtcNow,
                            iStatusCode = (int)HttpStatusCode.NotFound,
                            sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                            sErrorMessage = string.Format("Voor telwerk {0} is geen verbruiksregel geregistreerd.", counter.sCounterCode),
                            sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                        });
                        return serviceRun;
                    }

                    // Wat is de laatste datum in db van het verbruik
                    DateTime firstPickUpDay = lastConsumption.dtEndDateTime;

                    if (firstPickUpDay < authorizedFromDate)
                    {
                        serviceRun.ServiceRunErrors.Add(new ServiceRunError
                        {
                            dtEffectiveDateTime = DateTime.UtcNow,
                            iStatusCode = (int)HttpStatusCode.NotFound,
                            sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                            sErrorMessage = string.Format("Meter {0} is geauthorizeerd vanaf {1} en er wordt geprobeerd data op te halen vanaf {2}.", consumptionMeter.sEANCode, authorizedFromDate.Date, firstPickUpDay.Date),
                            sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                        });
                        return serviceRun;
                    }

                    // Haal de laatste beschikbare datum op uit de webservice
                    DateTime lastAvailableData = _fuduraService.GetLastAvailableData(consumptionMeter.sEANCode);

                    // Check of onze db meter hetzelfde meet als de meter uit de api
                    switch (counter.iCounterTypeKey)
                    {
                        case 1:
                            // Check of ws ook Elektra meet
                            if (_fuduraService.ProductType != "E")
                            {
                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                {
                                    dtEffectiveDateTime = DateTime.UtcNow,
                                    iStatusCode = (int)HttpStatusCode.NotFound,
                                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                    sErrorMessage = string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", _fuduraService.ProductType, counter.iCounterKey),
                                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                });
                                return serviceRun;
                            }
                            break;

                        case 2:
                            // Check of ws ook Gas meet
                            if (_fuduraService.ProductType != "G")
                            {
                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                {
                                    dtEffectiveDateTime = DateTime.UtcNow,
                                    iStatusCode = (int)HttpStatusCode.NotFound,
                                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                    sErrorMessage = string.Format("Vanuit Database verwachten we bij teller {1} voor gas en we krijgen nu een meter die {0} meet.", _fuduraService.ProductType, counter.iCounterKey),
                                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                });
                                return serviceRun;
                            }
                            break;

                        case 3:
                            // Check of ws ook Elektra meet
                            if (_fuduraService.ProductType != "E")
                            {
                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                {
                                    dtEffectiveDateTime = DateTime.UtcNow,
                                    iStatusCode = (int)HttpStatusCode.NotFound,
                                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                    sErrorMessage = string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", _fuduraService.ProductType, counter.iCounterKey),
                                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                });
                                return serviceRun;
                            }
                            break;

                        case 4:
                            // Check of ws ook Elektra meet
                            if (_fuduraService.ProductType != "E")
                            {
                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                {
                                    dtEffectiveDateTime = DateTime.UtcNow,
                                    iStatusCode = (int)HttpStatusCode.NotFound,
                                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                    sErrorMessage = string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", _fuduraService.ProductType, counter.iCounterKey),
                                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                });
                                return serviceRun;
                            }
                            break;

                    }

                    // Check tot welke datum opgehaald moet worden (vandaag of laatste registratiedatum)
                    DateTime lastPickUpDateTime = lastAvailableData < today ? lastAvailableData : today.Date;

                    TimeSpan timeSpan = lastPickUpDateTime - firstPickUpDay;
                    DateTime consumptionStart = firstPickUpDay;

                    for (int i = 0; i <= timeSpan.Days; i++)
                    {
                        //DateTime pickUpDay = firstPickUpDay.AddDays(i);
                        DateTime pickUpDay = consumptionStart;
                        decimal usage = 0;
                        DateTime consumptionDateTime = pickUpDay.Date;

                        string resultString = _fuduraService.GetTimeSeries(consumptionMeter.sEANCode, consumptionDateTime);
                        if (!string.IsNullOrEmpty(resultString))
                        {
                            JArray jsonArray = JArray.Parse(resultString);
                            bool saveConsumption = false;
                            foreach (JToken consumption in jsonArray)
                            {
                                //Voeg pas verbruik toe als de datum hoger is dan laatste datum in db
                                if (consumptionDateTime >= pickUpDay)
                                {
                                    saveConsumption = true;
                                    switch (counter.iCounterTypeKey)
                                    {
                                        case 3:
                                            usage = usage + decimal.Parse(consumption["ReadingL"].ToString());
                                            break;

                                        default:
                                            usage = usage + decimal.Parse(consumption["ReadingN"].ToString());
                                            break;
                                    }
                                }
                                consumptionDateTime = DateTime.Parse(consumption["Timestamp"].ToString());

                            }

                            Consumption previousConsumption = consumptionList.Where(w => w.Counter == counter).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault() != null ? consumptionList.Where(w => w.Counter == counter).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault() : lastConsumption;

                            Consumption newConsumption = new Consumption
                            {
                                iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                Counter = counter,
                                ServiceRun = serviceRun,
                                dtStartDateTime = consumptionStart,
                                dtEndDateTime = consumptionDateTime,
                                dConsumption = usage,
                                dEndPosition = previousConsumption.dEndPosition + usage,
                                bExcludeForReport = counter.ConsumptionMeter.MeterType.sPurchaseOrSale.Contains("Distribution"),
                                bValidated = true
                            };

                            if (saveConsumption)
                                consumptionList.Add(newConsumption);

                            consumptionStart = consumptionDateTime;
                        }
                        consumptionStart = consumptionStart.AddDays(1).Date;
                    } // End of for all days

                } // End of foreach counter       
                serviceRun.iServiceRunRowsUpdated += consumptionList.Count;
                db.Consumption.AddRange(consumptionList);
                await db.SaveChangesAsync();
                return await Task.FromResult(serviceRun);

            }
            catch (Exception e)
            {
                ServiceRunError error = new ServiceRunError
                {
                    dtEffectiveDateTime = DateTime.UtcNow,
                    iStatusCode = 500,
                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                    sErrorMessage = e.Message,
                    ServiceRun = serviceRun
                };
                serviceRun.ServiceRunErrors.Add(error);
                serviceRun.iServiceRunStatus = 500;

                return await Task.FromResult(serviceRun);

            } // Loop elk telwerk af

        }

        public async Task<ServiceRun> UpdateSDSConsuptionMeter(ConsumptionMeter consumptionMeter, ServiceRun serviceRun)
        {
            DateTime today = DateTime.Today;
            // Check if consumptionMeter exists in api
            if (await smartDataSolution.ConsumptionMeterExists(consumptionMeter.sEANCode))
            {
                if (consumptionMeter.Counters.Count > 0)
                {
                    // Get first day from api where data is stored
                    DateTime firstDayInApi = smartDataSolution.ConsumptionMeterAvailableFrom(consumptionMeter.sEANCode);
                    if (firstDayInApi != new DateTime())
                    {
                        foreach (Counter counter in consumptionMeter.Counters)
                        {
                            counter.Consumption = new List<Consumption>();
                            // Get lastConsumptionRow from database by this Counter
                            Consumption lastConsumption = db.Consumption.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dEndPosition).ThenByDescending(t => t.dtEndDateTime).FirstOrDefault();
                            if (lastConsumption != null)
                            {
                                // SET the first day for getting consumption
                                DateTime firstPickUpDateTime = lastConsumption.dtEndDateTime;
                                if (firstPickUpDateTime >= firstDayInApi)
                                {
                                    // Get last date from api where consumption is stored
                                    DateTime lastDayInApi = smartDataSolution.LastAvailableDateTime(consumptionMeter.sEANCode);
                                    if ((new int[] { 1, 3, 4 }.Contains(counter.iCounterTypeKey) && smartDataSolution.AssetType == "P4Electricity") || (counter.iCounterTypeKey == 2 && smartDataSolution.AssetType == "P4Gas"))
                                    {
                                        DateTime lastPickUpDateTime = lastDayInApi < today ? lastDayInApi : today;
                                        string webResult = smartDataSolution.GetConsumption(consumptionMeter.sEANCode, firstPickUpDateTime, lastPickUpDateTime);
                                        if (!string.IsNullOrEmpty(webResult))
                                        {
                                            try
                                            {
                                                JObject resultObject = JObject.Parse(webResult);
                                                JArray resultArray = JArray.Parse(resultObject["Payload"].ToString());
                                                foreach (JToken resultToken in resultArray)
                                                {
                                                    Consumption previousConsumption = counter.Consumption.Count > 0 ? counter.Consumption.OrderByDescending(o => o.dEndPosition).ThenByDescending(o => o.dtEndDateTime).FirstOrDefault() : lastConsumption;
                                                    decimal endPosition = 0;
                                                    switch (counter.iCounterTypeKey)
                                                    {
                                                        case 2:
                                                            decimal.TryParse(resultToken["R180"].ToString(), out endPosition);
                                                            break;
                                                        case 3:
                                                            decimal.TryParse(resultToken["R181"].ToString(), out endPosition);
                                                            break;
                                                        case 4:
                                                            decimal.TryParse(resultToken["R182"].ToString(), out endPosition);
                                                            break;
                                                    }

                                                    DateTime endPositionDateTime = DateTime.Parse(resultToken["DateTime"].ToString());

                                                    // Deal with correctionFactor
                                                    if (resultToken == resultArray.First)
                                                    {
                                                        counter.dCorrectionMutation = counter.dCorrectionMutation != 0 ? counter.dCorrectionMutation : previousConsumption.dEndPosition - endPosition;
                                                    }
                                                    else if (endPositionDateTime > previousConsumption.dtEndDateTime)
                                                    {
                                                        counter.Consumption.Add(new Consumption
                                                        {
                                                            iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                                            Counter = counter,
                                                            ServiceRun = serviceRun,
                                                            dtStartDateTime = previousConsumption.dtEndDateTime,
                                                            dtEndDateTime = endPositionDateTime,
                                                            dConsumption = endPosition - previousConsumption.dEndPosition,
                                                            dEndPosition = endPosition,
                                                            bExcludeForReport = counter.ConsumptionMeter.MeterType.sPurchaseOrSale.Contains("Distribution"),
                                                            bValidated = true
                                                        });
                                                        serviceRun.iServiceRunRowsUpdated++;
                                                        db.Entry(counter).State = EntityState.Modified;
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                                {
                                                    dtEffectiveDateTime = DateTime.UtcNow,
                                                    iStatusCode = (int)HttpStatusCode.InternalServerError,
                                                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                                    sErrorMessage = e.Message,
                                                    ServiceRun = serviceRun,
                                                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                                });
                                                serviceRun.iServiceRunStatus = 500;
                                            }

                                        }
                                        else
                                        {
                                            serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                            {
                                                dtEffectiveDateTime = DateTime.UtcNow,
                                                iStatusCode = (int)HttpStatusCode.NotFound,
                                                sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                                sErrorMessage = string.Format("De API heeft een foutmelding gegeven met startdatum {0} en einddatum {1}", firstPickUpDateTime, lastPickUpDateTime),
                                                ServiceRun = serviceRun,
                                                sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                            });
                                            serviceRun.iServiceRunStatus = 500;
                                        }
                                    }
                                    else
                                    {
                                        serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                        {
                                            dtEffectiveDateTime = DateTime.UtcNow,
                                            iStatusCode = (int)HttpStatusCode.NotFound,
                                            sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                            sErrorMessage = string.Format("Telwerk {0} is geregistreerd met type {1}, maar vanuit de API telt dit telwerk {2}", counter.sCounterCode, counter.CounterType.sCounterTypeDescription, smartDataSolution.AssetType),
                                            ServiceRun = serviceRun,
                                            sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                        });
                                        serviceRun.iServiceRunStatus = 500;
                                    }


                                }
                                else
                                {
                                    serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                    {
                                        dtEffectiveDateTime = DateTime.UtcNow,
                                        iStatusCode = (int)HttpStatusCode.NotFound,
                                        sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                        sErrorMessage = string.Format("Bij telwerk {0} met type {1} kan geen data opgehaald worden voor {2}. Geprobeerd wordt om verbruik op te halen vanaf {3}.", counter.sCounterCode, counter.CounterType.sCounterTypeDescription, firstDayInApi, firstPickUpDateTime),
                                        ServiceRun = serviceRun,
                                        sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                    });
                                    serviceRun.iServiceRunStatus = 500;
                                }
                            }
                            else
                            {
                                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                                {
                                    dtEffectiveDateTime = DateTime.UtcNow,
                                    iStatusCode = (int)HttpStatusCode.NotFound,
                                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                    sErrorMessage = string.Format("Bij telwerk {0} met type {1} kan geen laatste verbruiksregel opgehaald worden.", counter.sCounterCode, counter.CounterType.sCounterTypeDescription),
                                    ServiceRun = serviceRun,
                                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                                });
                                serviceRun.iServiceRunStatus = 500;
                            }
                        }
                    }
                    else
                    {
                        serviceRun.ServiceRunErrors.Add(new ServiceRunError
                        {
                            dtEffectiveDateTime = DateTime.UtcNow,
                            iStatusCode = (int)HttpStatusCode.NotFound,
                            sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                            sErrorMessage = "Van deze meter kan geen data opgehaald worden uit de API",
                            ServiceRun = serviceRun,
                            sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                        });
                        serviceRun.iServiceRunStatus = 500;
                    }
                }
                else
                {
                    serviceRun.ServiceRunErrors.Add(new ServiceRunError
                    {
                        dtEffectiveDateTime = DateTime.UtcNow,
                        iStatusCode = (int)HttpStatusCode.BadRequest,
                        sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                        sErrorMessage = "Bij deze meter zijn op dit moment geen telwerken gevonden",
                        ServiceRun = serviceRun,
                        sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                    });
                    serviceRun.iServiceRunStatus = 500;
                }
            }
            else
            {
                serviceRun.ServiceRunErrors.Add(new ServiceRunError
                {
                    dtEffectiveDateTime = DateTime.UtcNow,
                    iStatusCode = (int)HttpStatusCode.NotFound,
                    sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                    sErrorMessage = "Meter is niet gevonden in de API",
                    ServiceRun = serviceRun,
                    sProjectNumber = consumptionMeter.Address.Project.ProjectBase.sProjectCode
                });
                serviceRun.iServiceRunStatus = 500;
            }

            return await Task.FromResult(serviceRun);
        }

        public void Delete(PmsEteckContext context, bool save)
        {
            if (Counters.Count > 0)
            {
                //foreach (Consumption consumption in Counters.SelectMany(s => s.Consumption))
                //{
                //    context.Consumption.Attach(consumption);
                //    context.Consumption.Remove(consumption);
                //}
                context.Consumption.RemoveRange(Counters.SelectMany(s => s.Consumption));
                context.BlindConsumptions.RemoveRange(Counters.SelectMany(s => s.BlindConsumption));
                context.MaximumPowers.RemoveRange(Counters.SelectMany(s => s.MaximumPowers));
                context.CounterChangeLogs.RemoveRange(Counters.SelectMany(s => s.ChangeLogs));
                context.Counters.RemoveRange(Counters);
            }
            context.MeterChangeLogs.RemoveRange(MeterChangeLogs);
            context.ConsumptionMeters.Remove(this);

            if (save)
                context.SaveChanges(_httpContextAccessor.HttpContext.User.Identity.GetUserID());
        }
        #endregion
    }
}