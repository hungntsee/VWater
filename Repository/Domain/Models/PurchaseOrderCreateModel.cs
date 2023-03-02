namespace VWater.Domain.Models
{
    public partial class PurchaseOrderCreateModel
    {
        #region Generated Properties

        public int StoreId { get; set; }

        public int DistributorId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalQuantity { get; set; }

        public decimal ToatalPrice { get; set; }

        public int? Status { get; set; }

        public string Payment { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
