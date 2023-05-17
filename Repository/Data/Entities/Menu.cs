namespace VWater.Data.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            #region Generated Constructor
            ProductInMenus = new HashSet<ProductInMenu>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int StoreId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Store Store { get; set; }

        public virtual ICollection<ProductInMenu> ProductInMenus { get; set; }

        #endregion

    }
}
