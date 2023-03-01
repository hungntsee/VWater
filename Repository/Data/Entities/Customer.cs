namespace VWater.Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            #region Generated Constructor
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        #endregion

    }
}
