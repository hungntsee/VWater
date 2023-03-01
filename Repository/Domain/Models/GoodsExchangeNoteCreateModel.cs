namespace VWater.Domain.Models
{
    public partial class GoodsExchangeNoteCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public int WarehouseId { get; set; }

        public DateTime NoteDate { get; set; }

        public int GoodsId { get; set; }

        public int Quantity { get; set; }

        public int? Status { get; set; }

        public int? OrderId { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
