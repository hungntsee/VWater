namespace VWater.Domain.Models
{
    public class DepositNoteUpdateModel
    {
        public int OrderId { get; set; }

        public bool IsDeposit { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}
