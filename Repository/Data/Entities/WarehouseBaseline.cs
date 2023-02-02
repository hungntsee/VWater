using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class WarehouseBaseline
    {
        public WarehouseBaseline()
        {
            #region Generated Constructor
            GoodsInBaselines = new HashSet<GoodsInBaseline>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public DateTime Date { get; set; }

        public int TotalQuantity { get; set; }

        public int? Status { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<GoodsInBaseline> GoodsInBaselines { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        #endregion

    }
}
