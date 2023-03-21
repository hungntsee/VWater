namespace VWater.Domain.Models
{
    public partial class OrderCreateModel
    {
        #region Generated Properties
       

        public int DeliveryAddressId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int DeliverySlotId { get; set; }

        public ICollection<OrderDetailCreateModel> OrderDetails { get; set; }
        #endregion

    }
}
