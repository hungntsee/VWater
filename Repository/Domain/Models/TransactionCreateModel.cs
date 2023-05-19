using VWater.Data.Entities;

namespace VWater.Domain.Models
{
    public class TransactionCreateModel
    {
        public int WalletId { get; set; }
        public ICollection<OrderCreateModel> Orders { get; set; }
        public string? Note { get; set; }
        public int? Account_Id { get; set; }
        public int TransactionType_Id { get; set; }
    }
}
