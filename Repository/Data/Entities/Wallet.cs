namespace VWater.Data.Entities
{
    public partial class Wallet
    {
        public Wallet()
        {
            #region Generated Constructor
            Transactions = new HashSet<Transaction>();
            #endregion
        }
        public int Id { get; set; }
        public int ShipperId { get; set; }
        public decimal Credit { get; set; }

        #region Generated Relationships
        public virtual Shipper Shipper { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set;}
        #endregion
    }
}
