namespace VWater.Data.Entities
{
    public partial class Area
    {
        public Area()
        {
            #region Generated Constructor
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            Distributors = new HashSet<Distributor>();
            Menus = new HashSet<Menu>();
            Stores = new HashSet<Store>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string AreaName { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        public virtual ICollection<Distributor> Distributors { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        #endregion

    }
}
