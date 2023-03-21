namespace VWater.Data.Entities
{
    public partial class GoodsInProduct
    {
        public GoodsInProduct()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int? GoodsInBaselineId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual GoodsInBaseline GoodsInBaseline { get; set; }

        public virtual Product Product { get; set; }

        #endregion

    }
}
