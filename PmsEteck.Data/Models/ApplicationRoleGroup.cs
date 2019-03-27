//VLD many to many relationship between ApplicationRole - RoleGroup
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models.Results
{
    [Table("AspNetUserRolegroupRoles", Schema = "pms")]
    public class ApplicationRoleGroup
    {
        public string RoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }

        public int RoleGroupId { get; set; }
        public RoleGroup RoleGroup { get; set; }
    }
}
