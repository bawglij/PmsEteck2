namespace PmsEteck.Data.Models
{
    public class ProjectInfoSalesDeliveryType
    {
        public int ProjectInfoId { get; set; }
        public ProjectInfo ProjectInfo { get; set; }

        public int SalesDeliveryTypeId { get; set; }
        public SalesDeliveryType SalesDeliveryType { get; set; }

    }
}
