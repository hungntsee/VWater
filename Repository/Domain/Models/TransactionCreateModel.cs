namespace VWater.Domain.Models
{
    public class TransactionCreateModel
    {
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public int WalletId { get; set; }
        public int? OrderId { get; set; }
        public string? Note { get; set; }
        public int? Account_Id { get; set; }
    }
}
