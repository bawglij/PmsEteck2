using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BuildingManagementSystems", Schema = "meter")]
    public class BuildingManagementSystem
    {
        [Key]
        public int BuildingManagementSystemID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [StringLength(250)]
        public string TypeManager { get; set; }

        [StringLength(250)]
        public string ProjectName { get; set; }
        
        [StringLength(100)]
        public string IpPhoneNumber { get; set; }
        
        [StringLength(250)]
        public string Credentials { get; set; }

        [Display(Name = "Type communicatie")] 
        [ForeignKey("CommunicationType")]
        public int? CommunicationTypeID { get; set; }
        public virtual CommunicationType CommunicationType { get; set; }

        public virtual ProjectInfo Project { get; set; }
    }
}