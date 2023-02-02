using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class GoodsInQuotation
    {
        public GoodsInQuotation()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int QuotationId { get; set; }

        public int GoodsId { get; set; }

        public decimal Price { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Goods Goods { get; set; }

        public virtual Quotation Quotation { get; set; }

        #endregion

    }
}
