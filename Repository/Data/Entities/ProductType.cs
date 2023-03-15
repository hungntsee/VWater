namespace VWater.Data.Entities
{
    public partial class ProductType
    {
        public ProductType()
        {
            #region Generated Constructor
            Products = new HashSet<Product>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string ProductTypeName { get; set; }

        public string Url { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Product> Products { get; set; }

        #endregion

    }
}
