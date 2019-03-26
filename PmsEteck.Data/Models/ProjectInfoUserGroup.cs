namespace PmsEteck.Data.Models
{
    public class ProjectInfoUserGroup
    {
        public int ProjectInfoId { get; set; }
        public ProjectInfo ProjectInfo { get; set; }

        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

    }
}
