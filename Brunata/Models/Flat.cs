using System;
using System.Collections.ObjectModel;

namespace Brunata.Models
{
    public class Flat
    {
        public Flat()
        {
        }

        public string FlatNumber { get; set; }
        public DateTime? DisMantleDate { get; set; }
        public Address Address { get; set; }
        public int? ReductionPercent { get; set; }
        public Collection<Meter> Meters { get; set; }
    }
}