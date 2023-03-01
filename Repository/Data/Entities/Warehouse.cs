namespace VWater.Data.Entities
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            #region Generated Constructor
            GoodsExchangeNotes = new HashSet<GoodsExchangeNote>();
            WarehouseBaselines = new HashSet<WarehouseBaseline>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public int StoreId { get; set; }


        public string Capacity { get; set; }

        public string PhoneNumber { get; set; }

        #endregion

        #region Generated Relationships

        public virtual ICollection<GoodsExchangeNote> GoodsExchangeNotes { get; set; }

        public virtual Store Store { get; set; }

        public virtual ICollection<WarehouseBaseline> WarehouseBaselines { get; set; }

        #endregion

    }
}
