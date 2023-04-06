namespace VWater.Data.Entities
{
    public class Transaction
    {
        public Transaction() 
        {
            #region Generated Constructor
            #endregion
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int WalletId { get; set; }
        public int? OrderId { get; set; }
        public string Note { get; set; }
        public int AccountId { get; set; }

        #region Generated Relationships
        public virtual Order Order { get; set; }

        public virtual Wallet Wallet { get; set; }
        public virtual Account Account { get; set; }
        #endregion
    }
}
