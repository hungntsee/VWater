using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Status
    {
        public Status()
        {
            #region Generated Constructor
            Orders = new HashSet<Order>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string StatusName { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        #endregion

    }
}
