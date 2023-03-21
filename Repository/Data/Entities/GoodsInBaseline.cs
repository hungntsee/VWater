namespace VWater.Data.Entities
{
    public partial class GoodsInBaseline
    {
        public GoodsInBaseline()
        {
            #region Generated Constructor
            GoodsInProducts = new HashSet<GoodsInProduct>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int WarehouseBaselineId { get; set; }

        public int GoodsId { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Goods Goods { get; set; }

        public virtual ICollection<GoodsInProduct> GoodsInProducts { get; set; }

        public virtual WarehouseBaseline WarehouseBaseline { get; set; }

        #endregion

    }
}
