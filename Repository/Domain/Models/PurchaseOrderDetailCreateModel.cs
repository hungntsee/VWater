namespace VWater.Domain.Models
{
    public partial class PurchaseOrderDetailCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public int GoodsInQuotationId { get; set; }

        public int Quantity { get; set; }

        #endregion

    }
}
