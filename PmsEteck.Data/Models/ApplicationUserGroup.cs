//VLD
namespace PmsEteck.Data.Models
{
    public class ApplicationUserGroup
    {
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int GroupId { get; set; }
        public UserGroup UserGroup { get; set; }

    }
}
