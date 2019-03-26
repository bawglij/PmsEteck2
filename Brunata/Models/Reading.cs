using RestSharp.Deserializers;
using System;

namespace Brunata.Models
{
    public class Reading
    {
        public Reading()
        {
        }

        [DeserializeAs(Name = "value")]
        public double Value { get; set; }

        [DeserializeAs(Name = "unit")]
        public string Unit { get; set; }

        //public double YearReading { get; set; }

        [DeserializeAs(Name = "readingDate")]
        public DateTime ReadingDateTime { get; set; }

    }
}
