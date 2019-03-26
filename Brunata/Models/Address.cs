namespace Brunata.Models
{
    public class Address
    {
        public Address()
        {
        }

        public string Street { get; set; }
        public string Stairway { get; set; }
        public string Floor { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}