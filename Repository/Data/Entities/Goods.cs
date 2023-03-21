namespace VWater.Data.Entities
{
    public partial class Goods
    {
        public Goods()
        {
            #region Generated Constructor
            GoodsCompositions = new HashSet<GoodsComposition>();
            GoodsExchangeNotes = new HashSet<GoodsExchangeNote>();
            GoodsInBaselines = new HashSet<GoodsInBaseline>();
            GoodsInQuotations = new HashSet<GoodsInQuotation>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string GoodsName { get; set; }

        public string Img { get; set; }

        public string Volume { get; set; }

        public string Note { get; set; }

        public int BrandId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Brand Brand { get; set; }

        public virtual ICollection<GoodsComposition> GoodsCompositions { get; set; }

        public virtual ICollection<GoodsExchangeNote> GoodsExchangeNotes { get; set; }

        public virtual ICollection<GoodsInBaseline> GoodsInBaselines { get; set; }

        public virtual ICollection<GoodsInQuotation> GoodsInQuotations { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        #endregion

    }
}
