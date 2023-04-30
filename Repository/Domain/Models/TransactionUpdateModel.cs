﻿namespace Repository.Domain.Models
{
    public class TransactionUpdateModel
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int WalletId { get; set; }
        public int? OrderId { get; set; }
        public string? Note { get; set; }
        public int? Account_Id { get; set; }
        public int? Transaction_Id { get; set; }
    }
}
