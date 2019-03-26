using RestSharp.Deserializers;
using System;
using System.Collections.ObjectModel;

namespace Brunata.Models
{
    public class Object
    {
        public Object()
        {
        }

        public Object(string systemNumber)
        {
            SystemNumber = systemNumber;
        }

        public string SystemNumber { get; set; }
        public string Name { get; set; }
        public DateTime StateTimestamp { get; set; }

        [DeserializeAs(Name = "properties")]
        public Collection<Property> Properties { get; set; }

    }
}
