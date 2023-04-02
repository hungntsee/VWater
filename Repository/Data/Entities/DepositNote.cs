namespace VWater.Data.Entities
{
    public class DepositNote
    {
        public DepositNote()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public bool IsDeposit { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Order Order { get; set; }

        #endregion
    }
}
