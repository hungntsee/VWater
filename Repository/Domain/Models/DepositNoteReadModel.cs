namespace Repository.Domain.Models
{
    public class DepositNoteReadModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public bool IsDeposit { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
