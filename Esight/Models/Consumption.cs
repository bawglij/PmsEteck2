using System;

namespace Esight.Models
{
    public class Consumption
    {
        public int RowNumber { get; set; }
        public DateTime DateTime { get; set; }
        public decimal DayConsumption { get; set; }
        public decimal? NightConsumption { get; set; }
    }
}
