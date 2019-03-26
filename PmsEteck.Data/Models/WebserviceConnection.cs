using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    public class WebserviceConnection : BaseModel
    {
        [ForeignKey(nameof(MaintenanceContact))]
        public int MaintenanceContactID { get; set; }

        public MaintenanceContactCommunicationType MaintenanceContactCommunicationType { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        [StringLength(256)]
        public string Domain { get; set; }

        [StringLength(512)]
        public string BaseUrl { get; set; }

        public virtual MaintenanceContact MaintenanceContact { get; set; }
    }
}
