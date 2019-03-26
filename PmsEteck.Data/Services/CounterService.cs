using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    class CounterService
    {
        private PmsEteckContext db = new PmsEteckContext();

        private Counter counter { get; set; }

        public async Task<Result> UpdateCounter(int counterID)
        {
            counter = await db.Counters
                .FirstOrDefaultAsync(f => f.iCounterKey == counterID);

            if (counter == null)
                return await Task.FromResult(Result.Failed("Telwerk kan niet gevonden worden."));

            if (!counter.iConsumptionMeterKey.HasValue)
                return await Task.FromResult(Result.Failed("Telwerk is niet gekoppeld aan een meter."));

            if (!counter.ConsumptionMeter.iAddressKey.HasValue)
                return await Task.FromResult(Result.Failed("Meter is niet gekoppeld aan een adres"));

            BlindConsumption blindConsumption = new BlindConsumption();
            MaximumPower maximumPower = new MaximumPower();
            Consumption consumption = new Consumption();
            switch (counter.iCounterTypeKey)
            {
                case 11:
                case 14:
                    maximumPower = await GetLastMaximumPowerAsync();
                    break;
                case 15:
                    blindConsumption = await GetLastBlindConsumptionAsync();
                    break;
                default:
                    consumption = await GetLastConsumptionAsync();
                    break;
            }

            return await Task.FromResult(Result.Success);
        }

        public async Task<Consumption> GetLastConsumptionAsync()
        {
            return await db.Consumption
                .OrderByDescending(o => o.dtEndDateTime)
                .ThenByDescending(o => o.dEndPosition)
                .FirstOrDefaultAsync(f => f.iCounterKey == counter.iCounterKey);
        }

        public async Task<MaximumPower> GetLastMaximumPowerAsync()
        {
            return await db.MaximumPowers
                .OrderByDescending(o => o.dtEndDateTime)
                .FirstOrDefaultAsync(f => f.iCounterKey == counter.iCounterKey);
        }

        public async Task<BlindConsumption> GetLastBlindConsumptionAsync()
        {
            return await db.BlindConsumptions
                .OrderByDescending(o => o.dtEndDateTime)
                .FirstOrDefaultAsync(f => f.iCounterKey == counter.iCounterKey);
        }
    }
}
