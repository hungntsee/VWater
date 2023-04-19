namespace VWater.Domain.Models
{
    public class ReportOrderResponseModel
    {
        public int NumberOfFinishOrder { get; set; }
        public int NumberOfWaitingOrder { get; set; }
        public int NumberOfConfirmedOrder { get; set; }
        public int NumberOfShippingOrder { get; set; }
        public int NumberOfFailOrder { get; set; }

    }
}
