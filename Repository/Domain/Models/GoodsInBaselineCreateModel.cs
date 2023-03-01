namespace VWater.Domain.Models
{
    public partial class GoodsInBaselineCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int WarehouseBaselineId { get; set; }

        public int GoodsId { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
