using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class ConsumptionService
    {
        private PmsEteckContext context;
        private DateTime FirstPeriodDate;
        private DateTime LastPeriodDate;
        private decimal Consumption = 0;
        private List<BlindConsumption> BlindConsumptionList;
        private List<MaximumPower> MaximumPowerList;
        private List<Consumption> ConsumptionList;
        public List<MaximumPower> MaximumPowers { get; private set; }
        public List<BlindConsumption> BlindConsumptions { get; private set; }
        public List<Consumption> Consumptions { get; private set; }

        public ConsumptionService(PmsEteckContext eteckContext, Address address)
        {
            context = eteckContext ?? throw new ArgumentNullException(nameof(eteckContext));
            BlindConsumptionList = context.BlindConsumptions.Where(w => w.iAddressKey == address.iAddressKey).ToList();
            BlindConsumptions = BlindConsumptionList.OrderBy(o => o.dtEndDateTime).ThenBy(o => o.dBlindConsumption).ToList();
            MaximumPowerList =  context.MaximumPowers.Where(w => w.iAddressKey == address.iAddressKey).ToList();
            MaximumPowers = MaximumPowerList.OrderBy(o => o.dtEndDateTime).ThenBy(o => o.dMaximumPower).ToList();
            ConsumptionList = context.Consumption.Where(w => w.iAddressKey == address.iAddressKey).ToList();
            Consumptions = ConsumptionList.OrderBy(o => o.dtEndDateTime).ThenBy(o => o.dEndPosition).ToList();
        }

        public decimal GetConsumption(RateCardRow rateCardRow, DateTime firstPeriodDate, DateTime lastPeriodDate)
        {
            FirstPeriodDate = firstPeriodDate;
            LastPeriodDate = lastPeriodDate;
            int numberOfDays = (lastPeriodDate - firstPeriodDate).Days;
            Consumptions = Consumptions.Where(w => (w.dtStartDateTime >= FirstPeriodDate && w.dtStartDateTime < LastPeriodDate) || (w.dtEndDateTime > FirstPeriodDate && w.dtEndDateTime <= LastPeriodDate)).ToList();
            MaximumPowers = MaximumPowers.Where(w => (w.dtStartDateTime >= FirstPeriodDate && w.dtStartDateTime < LastPeriodDate) || (w.dtEndDateTime > FirstPeriodDate && w.dtEndDateTime <= LastPeriodDate)).ToList();
            BlindConsumptions = BlindConsumptions.Where(w => (w.dtStartDateTime >= FirstPeriodDate && w.dtStartDateTime < LastPeriodDate) || (w.dtEndDateTime > FirstPeriodDate && w.dtEndDateTime <= LastPeriodDate)).ToList();
            switch (rateCardRow.iCounterTypeKey)
            {
                case 10:
                    // Elektra - Som
                    Consumptions = Consumptions.Where(w => w.Counter.iUnitKey == rateCardRow.iUnitKey && new int[] { 1, 3, 4 }.Contains(w.Counter.iCounterTypeKey)).ToList();
                    break;
                case 11:
                case 14:
                    // Elektra - Max vermogen
                    // Gas - Max vermogen
                    MaximumPowers = MaximumPowers.Where(w => w.Counter.iUnitKey == rateCardRow.iUnitKey && w.Counter.iCounterTypeKey == rateCardRow.iCounterTypeKey).ToList();
                    break;
                case 15:
                    // Elektra - Blindvermogen
                    BlindConsumptions = BlindConsumptions.Where(w => w.Counter.iUnitKey == rateCardRow.iUnitKey && w.Counter.iCounterTypeKey == rateCardRow.iCounterTypeKey).ToList();
                    Consumptions = Consumptions.Where(w => w.Counter.iUnitKey == 2 && new int[] { 3, 4 }.Contains(w.Counter.iCounterTypeKey)).ToList();
                    break;
                default:
                    Consumptions = Consumptions.Where(w => w.Counter.iCounterTypeKey == rateCardRow.iCounterTypeKey && w.Counter.iUnitKey == rateCardRow.iUnitKey).ToList();
                    break;
            }
            foreach (Consumption consumption in Consumptions)
            {
                consumption.dtEndDateTime = consumption.dtEndDateTime.Date;
                consumption.dtStartDateTime = consumption.dtStartDateTime.Date;
                int consumptionDays = (consumption.dtEndDateTime - consumption.dtStartDateTime).Days;

                // Voer berekening uit als de verbruiksregel over meer dan 1 dag gaat, anders moet het hele verbruik meegenomen worcden.
                if (consumptionDays > 0)
                {
                    if (consumption.dtStartDateTime <= firstPeriodDate && consumption.dtEndDateTime >= lastPeriodDate)
                    {
                        consumption.dConsumption = consumption.dConsumption / consumptionDays * numberOfDays;
                    }
                    else if (consumption.dtStartDateTime <= firstPeriodDate)
                    {
                        TimeSpan activeTimeSpan = consumption.dtEndDateTime.AddDays(1) - firstPeriodDate;
                        consumption.dConsumption = consumption.dConsumption / consumptionDays * activeTimeSpan.Days;
                    }
                    else if (consumption.dtEndDateTime >= lastPeriodDate)
                    {
                        TimeSpan activeTimeSpan = lastPeriodDate.AddDays(1) - consumption.dtStartDateTime;
                        consumption.dConsumption = consumption.dConsumption / consumptionDays * activeTimeSpan.Days;
                    }
                }
            }

            switch (rateCardRow.iCounterTypeKey)
            {
                case 11:
                case 14:
                    Consumption = MaximumPowers.Sum(sm => sm.dMaximumPower);
                    break;
                case 15:
                    Consumption = (BlindConsumptions.Sum(sm => sm.dBlindConsumption) - (Consumptions.Sum(sm => sm.dConsumption) * (decimal)0.62));
                    break;
                default:
                    Consumption = Consumptions.Sum(sm => sm.dConsumption);
                    break;
            }
            return Consumption;
        }
    }
}
