namespace VWater.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            #region Generated Constructor
            OrderDetails = new HashSet<OrderDetail>();
            Transactions = new HashSet<Transaction>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int DeliveryAddressId { get; set; }

        public int StoreId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal AmountPaid { get; set; }

        public int? StatusId { get; set; }

        public int DeliverySlotId { get; set; }

        public int? ShipperId { get; set; }

        public bool? IsDeposit { get; set; }

        public string? OrderIdMomo { get; set; }

        public string? IpnData { get; set; }

        #endregion

        #region Generated Relationships
        public virtual DeliveryAddress DeliveryAddress { get; set; }

        public virtual DeliverySlot DeliverySlot { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Shipper Shipper { get; set; }

        public virtual Status Status { get; set; }

        public virtual Store Store { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual DepositNote? DepositNote { get; set; }

        #endregion

    }
}
