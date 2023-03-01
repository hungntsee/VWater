namespace VWater.Domain.Models
{
    public partial class DeliverySlotReadModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string SlotName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan TimeFrom { get; set; }

        public TimeSpan TimeTo { get; set; }

        public int StoreId { get; set; }

        #endregion

    }
}
