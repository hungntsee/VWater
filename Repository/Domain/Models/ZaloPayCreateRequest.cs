using Newtonsoft.Json;
using ZaloPay.Helper;
using ZaloPay.Helper.Crypto;

namespace VWater.Domain.Models
{
    public class ZaloPayCreateRequest
    {
        public string AppId { get; set; }
        public string AppUser { get; set; }
        public string AppTransId { get; set; }
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public string EmbedData { get; set; }
        public string BankCode { get; set; }
        public string mac { get; set; }
        public string callback_url { get; set; }


        public ZaloPayCreateRequest(long amount, object embeddata, object item, IConfiguration configuration)
        {
            AppId = configuration["ZaloPay:Appid"];
            AppTransId = Utils.GenTransID();
            AppTime = Utils.GetTimeStamp();
            AppUser = "VWater Shop";
            Amount = amount;
            BankCode = "zalopayapp";
            Description = "Thanh toán đơn hàng tại VWater Shop";
            EmbedData = JsonConvert.SerializeObject(embeddata);
            Item = JsonConvert.SerializeObject(item);
            mac = ComputeMac(configuration);
        }

        public virtual string GetMacData()
        {
            return AppId + "|" + AppTransId + "|" + AppUser + "|" + Amount + "|" + AppTime + "|" + EmbedData + "|" + Item;
        }

        public string ComputeMac(IConfiguration configuration)
        {
            return HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, configuration["ZaloPay:Key1"], GetMacData());
        }       
    }
}
