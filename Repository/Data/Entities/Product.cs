namespace VWater.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            #region Generated Constructor
            ProductInMenus = new HashSet<ProductInMenu>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public int? ProductType_Id { get; set; }

        public int? BrandId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Brand? Brand { get; set; }

        public virtual ICollection<ProductInMenu> ProductInMenus { get; set; }

        public virtual ProductType? ProductType { get; set; }

        #endregion

    }
}
