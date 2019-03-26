using System.Collections.ObjectModel;

namespace Brunata.Models
{
    public class Branch
    {
        public Branch()
        {
        }

        public string BrancheNumber { get; set; }
        public Collection<Flat> Flats { get; set; }
    }
}