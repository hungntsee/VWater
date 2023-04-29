namespace VWater.Data.Entities
{
    public class ProductsInBaseline
    {

        public ProductsInBaseline()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int WarehouseBaselineId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }
        #endregion

        #region Generated Relationships
        public virtual WarehouseBaseline WarehouseBaseline { get; set; }

        public virtual Product Product { get; set; }
        #endregion
    }
}
