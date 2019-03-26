namespace PmsEteck.Data.Models
{
    public class ProjectInfoPurchDeliveryType
    {
        public int ProjectInfoPurchId { get; set; }
        public ProjectInfo ProjectInfo { get; set; }

        public int PurchaseDeliveryTypeId { get; set; }
        public PurchaseDeliveryType PurchaseDeliveryType { get; set; }
    }
}
