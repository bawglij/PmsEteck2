//VLD many to many relationship between ApplicationRole - RoleGroup
namespace PmsEteck.Data.Models.Results
{
    public class ApplicationRoleGroup
    {
        public int UserId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }

        public int RoleId { get; set; }
        public RoleGroup RoleGroup { get; set; }

    }
}
