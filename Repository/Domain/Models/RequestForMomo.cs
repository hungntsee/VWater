namespace VWater.Domain.Models
{
    public class RequestForMomo
    {
        public string PartnerCode { get; set; }
        public string PartnerName { get; set;}
        public string StoreId { get; set;}
        public string RequestId { get; set;}

        public string RequestType = "captureWallet";
        public string OrderId { get; set;}

        public string OrderInfo = "Thanh toán đơn hàng tại VWater ";
        public long Amount { get; set;}
        public string RedirectUrl { get; set;}
        public string IpnUrl { get; set;}
        public string ExtraData { get; set;}

        public string Lang = "vi";
        public string Signature { get; set;}
    }
}
