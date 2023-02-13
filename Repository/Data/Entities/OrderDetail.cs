using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        #endregion

    }
}