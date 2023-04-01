namespace VWater.Domain.Models
{
    public class DepositNoteCreateModel
    {
        public bool isDeposit { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; } 

    }
}
