namespace VWater.Data.Entities
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            #region Generated Constructor
            Transactions = new HashSet<Transaction>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string TransactionTypeName { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Transaction> Transactions { get; set; }

        #endregion

    }
}
