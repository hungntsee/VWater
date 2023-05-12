namespace VWater.Data.Entities
{
    public partial class Brand
    {
        public Brand()
        {
            #region Generated Constructor
            Products = new HashSet<Product>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string BrandName { get; set; }

        public string Logo { get; set; }

        public string Origin { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Product> Products { get; set; }

        #endregion

    }
}
