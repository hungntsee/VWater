using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class GoodsInQuotationCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int QuotationId { get; set; }

        public int GoodsId { get; set; }

        public decimal Price { get; set; }

        #endregion

    }
}
