using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class ProductInMenu
    {
        public ProductInMenu()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int MenuId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Menu Menu { get; set; }

        public virtual Product Product { get; set; }

        #endregion

    }
}
