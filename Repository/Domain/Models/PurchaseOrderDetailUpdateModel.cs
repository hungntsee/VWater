using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class PurchaseOrderDetailUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public int GoodsId { get; set; }

        public int Quantity { get; set; }

        #endregion

    }
}
