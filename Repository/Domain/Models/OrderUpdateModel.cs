namespace VWater.Domain.Models
{
    public partial class OrderUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int DeliveryAddressId { get; set; }

        public int StoreId { get; set; }

        public DateTime OrderDate { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int? Status { get; set; }

        public int DeliverySlotId { get; set; }
        public string PhoneNumber { get; set; }
        #endregion

    }
}
