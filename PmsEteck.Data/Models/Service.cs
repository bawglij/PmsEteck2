
using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models.Results;
using PmsEteck.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table(name: "Services", Schema = "meter")]
    public class Service
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        private SmartDataSolution smartDataSolution = new SmartDataSolution();
        private ConsumptionMeterService meterService = new ConsumptionMeterService();
        #endregion
        #region Properties
        [Key]
        public int iServiceKey { get; set; }

        [Required]
        [MaxLength(100)]
        public string sServiceName { get; set; }

        public DateTime? dtNextServiceRun { get; set; }

        [Display(Name = "Servicetype")]
        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sServiceType { get; set; }
        
        public ICollection<ServiceRun> ServiceRuns { get; set; }
        #endregion
        #region Methods
        public async Task<Result> SDSServiceAsync()
        {
            ServiceRuns = new List<ServiceRun>();
            ServiceRun serviceRun = new ServiceRun
            {
                dtServiceRunStartDate = DateTime.UtcNow,
                iServiceKey = 1,
                iServiceRunRowsUpdated = 0,
                iServiceRunStatus = 200,
                ServiceRunErrors = new List<ServiceRunError>(),
                sServiceRunMessage = string.Empty
            };

            try
            {
                DateTime today = DateTime.Today;

                // Get all active consumptionMeters where supplier is SDS
                List<ConsumptionMeter> activeConsumptionMeters = await db.ConsumptionMeters
                    .Include(i => i.Counters)
                    .Where(w => w.iConsumptionMeterSupplierKey == 1 && w.iAddressKey.HasValue).ToListAsync();
                foreach (ConsumptionMeter consumptionMeter in activeConsumptionMeters.Where(w => w.iConsumptionMeterKey == 2767))
                {
                    try
                    {
                        serviceRun = await consumptionMeter.UpdateSDSConsuptionMeter(consumptionMeter, serviceRun);
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

                if (serviceRun.iServiceRunRowsUpdated > 0)
                {
                    serviceRun.sServiceRunMessage = serviceRun.iServiceRunRowsUpdated + " regels toegevoegd.";
                    serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                    db.ServiceRuns.Add(serviceRun);
                }
                await db.SaveChangesAsync();
                return await Task.FromResult(Result.Success);
            }
            catch (Exception e)
            {
                return await Task.FromResult(Result.Failed(e.Message));
            }

        }

        public async Task<ServiceRun> NewServiceRunAsync(List<Counter> counters, PmsEteckContext db)
        {
            ServiceRun serviceRun = new ServiceRun {
                dtServiceRunStartDate = DateTime.UtcNow,
                iServiceKey = iServiceKey,
                iServiceRunRowsUpdated = 0,
                iServiceRunStatus = 200,
                ServiceRunErrors = new List<ServiceRunError>()
            };
            meterService = new ConsumptionMeterService(db);
            switch (iServiceKey)
            {
                case 16:
                    serviceRun = await meterService.AddConsumptionsAsync(counters, serviceRun);
                    serviceRun.sServiceRunMessage = serviceRun.iServiceRunRowsUpdated + " verbruiksregels toegevoegd.";
                    break;
                case 19:
                    serviceRun = await meterService.RemoveConsumptionLastTwoWeeksAsync(counters, serviceRun, db);
                    int removedRows = serviceRun.iServiceRunRowsUpdated;
                    serviceRun = await meterService.AddConsumptionsAsync(counters, serviceRun);
                    serviceRun.sServiceRunMessage = string.Format("{0} regels verwijderd en {1} regels toegevoegd", removedRows, serviceRun.iServiceRunRowsUpdated - removedRows);
                    break;
                case 20:
                    serviceRun = await meterService.AddBrunataStandingsAsync(counters, serviceRun, db);
                    break;
                default:
                    break;
            }

            serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
            serviceRun.iServiceRunStatus = serviceRun.ServiceRunErrors.Any(u => u.iStatusCode == 500) ? 500 : 200;

            return await Task.FromResult(serviceRun);
        }
        #endregion
    }
}