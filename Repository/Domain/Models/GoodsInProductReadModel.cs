using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class GoodsInProductReadModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int GoodId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        #endregion

    }
}