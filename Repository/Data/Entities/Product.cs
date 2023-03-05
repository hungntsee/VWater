namespace VWater.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            #region Generated Constructor
            GoodsInProducts = new HashSet<GoodsInProduct>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductInMenus = new HashSet<ProductInMenu>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public int ProductType_Id { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<GoodsInProduct> GoodsInProducts { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<ProductInMenu> ProductInMenus { get; set; }
        public virtual ProductType ProductType { get; set; }

        #endregion

    }
}
