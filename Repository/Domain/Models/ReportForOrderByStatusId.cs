namespace VWater.Domain.Models
{
    public class ReportForOrderByStatusId
    {
        public int WaitingOrder { get; set; }
        public int ApprovedOrder { get; set; }
        public int PayingOrder { get; set; }
        public int PaidOrder { get; set; }
        public int ShippingOrder { get; set; }
        public int ShippedOrder { get; set; }
        public int CancelOrder { get; set; }
        public int FinishOrder { get; set; }
        public int TotalOrder { get; set; }
    }
}
