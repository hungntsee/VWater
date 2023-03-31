namespace VWater.Data.Entities
{
    public partial class Shipper
    {
        public Shipper()
        {
            #region Generated Constructor
            Orders = new HashSet<Order>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Fullname { get; set; }

        public string PhoneNumber { get; set; }

        public int StoreId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Account Account { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual Store Store { get; set; }
        public virtual Wallet Wallet { get; set; }

        #endregion

    }
}
