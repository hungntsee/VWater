namespace VWater.Data.Entities
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            #region Generated Constructor
            GoodsExchangeNotes = new HashSet<GoodsExchangeNote>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            #endregion
        }

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

        #region Generated Relationships
        public virtual Distributor Distributor { get; set; }

        public virtual ICollection<GoodsExchangeNote> GoodsExchangeNotes { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public virtual Store Store { get; set; }

        #endregion

    }
}
