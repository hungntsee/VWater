using System.Text.Json.Serialization;

namespace VWater.Domain.Models
{
    public class ZaloPayCreateRequest
    {
        public string app_id { get; set; }

        public string app_user = "VWater Shop";
        public string app_trans_id { get; set; }
        public string app_time { get; set; }
        public string amount { get; set; }

        public string item = "[]";

        public string desciption = "Thanh toán đơn hàng tại VWater Shop";
        public string embed_data { get; set; }

        public string bank_code = "zalopayapp";
        public string mac { get; set; }
        public string callback_url { get; set;}

    }
}
