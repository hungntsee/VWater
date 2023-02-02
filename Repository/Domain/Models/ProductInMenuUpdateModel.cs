using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class ProductInMenuUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int MenuId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        #endregion

    }
}
