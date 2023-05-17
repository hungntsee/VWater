namespace VWater.Data.Entities
{
    public partial class Area
    {
        public Area()
        {
            #region Generated Constructor
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            Stores = new HashSet<Store>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string AreaName { get; set; }

        public bool? IsActive { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        #endregion

    }
}
