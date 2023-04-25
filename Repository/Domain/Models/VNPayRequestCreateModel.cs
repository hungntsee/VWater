using System.Security.Cryptography;
using System.Text;

namespace VWater.Domain.Models
{
    public class VNPayRequestCreateModel
    {
        public string Version { get; set; }
        public string Command { get; set; }
        public string TmnCode { get; set; }
        public long Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string CurrCode { get; set; }
        public string IpAddr { get; set; }
        public string Locale { get; set; }
        public string OrderInfo { get; set; }
        public string ReturnUrl { get; set; }
        public DateTime ExpireDate { get; set; }
        public string TxnRef { get; set; }
        public string SercureHash { get; set; }

        public VNPayRequestCreateModel(string tmnCode,long amount, DateTime createDate, string ipAddr, string orderInfo, IConfiguration config, string txnRef, string sercureCode) 
        {
            Version = "2.1.0";
            Command = "pay";
            TmnCode = tmnCode;
            Amount = amount;
            CreateDate = createDate;
            CurrCode = "VND";
            IpAddr = ipAddr;
            Locale = "vn";
            OrderInfo = orderInfo;
            ReturnUrl = config["VNPay:vnp_Returnurl"];
            TxnRef = txnRef;
            SercureHash = sercureCode;
        }

        private static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
