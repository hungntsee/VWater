using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class GoodsExchangeNote
    {
        public GoodsExchangeNote()
        {
            #region Generated Constructor
            #endregion
        }

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

        #region Generated Relationships
        public virtual Goods Goods { get; set; }

        public virtual Order Order { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        #endregion

    }
}
