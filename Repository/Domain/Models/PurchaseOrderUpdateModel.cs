namespace VWater.Domain.Models
{
    public partial class PurchaseOrderUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

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
