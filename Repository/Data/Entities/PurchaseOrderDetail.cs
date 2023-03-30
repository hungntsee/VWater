namespace VWater.Data.Entities
{
    public partial class PurchaseOrderDetail
    {
        public PurchaseOrderDetail()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public int GoodsInQuotationId { get; set; }

        public int Quantity { get; set; }

        #endregion

        #region Generated Relationships
        public virtual GoodsInQuotation GoodsInQuotation { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        #endregion

    }
}
