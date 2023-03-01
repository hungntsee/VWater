namespace VWater.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            #region Generated Constructor
            GoodsExchangeNotes = new HashSet<GoodsExchangeNote>();
            OrderDetails = new HashSet<OrderDetail>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int DeliveryAddressId { get; set; }

        public int StoreId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int? Status { get; set; }

        public int DeliverySlotId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual DeliveryAddress DeliveryAddress { get; set; }

        public virtual DeliverySlot DeliverySlot { get; set; }

        public virtual ICollection<GoodsExchangeNote> GoodsExchangeNotes { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Store Store { get; set; }

        #endregion

    }
}
