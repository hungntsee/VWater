using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class PurchaseOrderDetail
    {
        public PurchaseOrderDetail()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public int GoodsId { get; set; }

        public int Quantity { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Goods Goods { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        #endregion

    }
}
