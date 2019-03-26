namespace Esight.Models
{
    public class Site
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public int ParentTypeID { get; set; }
        public int ParentID { get; set; }
    }
}
