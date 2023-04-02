﻿namespace Repository.Domain.Models
{
    public class TransactionUpdateModel
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int WalletId { get; set; }
        public int? OrderId { get; set; }
        public string? Note { get; set; }
    }
}