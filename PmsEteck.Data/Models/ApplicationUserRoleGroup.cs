//VLD
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AspNetUserRoleGroups", Schema = "pms")]
    public class ApplicationUserRoleGroup
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int RoleGroupId { get; set; }
        public RoleGroup RoleGroup { get; set; }
    }
}
