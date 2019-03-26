using System.Collections.ObjectModel;

namespace Brunata.Models
{
    public class Property
    {
        public Property()
        {
        }

        public string PropertyNumber { get; set; }
        public Collection<Branch> Branches { get; set; }
    }
}