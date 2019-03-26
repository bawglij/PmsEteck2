using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    class EsightService
    {
        private PmsEteckContext _context = new PmsEteckContext();

        public EsightService()
        {
        }

        public async Task UpdateAllMeters()
        {
            ServiceRun serviceRun = new ServiceRun() {
                dtServiceRunStartDate = DateTime.UtcNow,
                iServiceKey = 16,
                iServiceRunRowsUpdated = 0,
                iServiceRunStatus = (int)HttpStatusCode.OK,
                ServiceRunErrors = new List<ServiceRunError>()
            };
            List<ConsumptionMeter> consumptionMeterList = await _context.ConsumptionMeters.Include(i => i.Counters.Select(s => s.Consumption)).Where(w => w.iConsumptionMeterSupplierKey == 6).ToListAsync();

        }

        public async Task<ServiceRun> UpdateMeter(ConsumptionMeter consumptionMeter, ServiceRun serviceRun)
        {
            //Create an serviceRunError for this meter
            ServiceRunError runError = new ServiceRunError {
                iStatusCode = (int)HttpStatusCode.OK,
                sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber
            };

            serviceRun.ServiceRunErrors.Add(runError);
            return await Task.FromResult(serviceRun);
        }
    }
}
