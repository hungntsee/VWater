namespace VWater.Data.Entities
{
    public partial class Building
    {
        public Building()
        {
            #region Generated Constructor
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string BuildingName { get; set; }

        public int ApartmentId { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Apartment Apartment { get; set; }

        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        #endregion

    }
}
