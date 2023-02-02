using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class DeliverySlot
    {
        public DeliverySlot()
        {
            #region Generated Constructor
            Orders = new HashSet<Order>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string SlotName { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan TimeFrom { get; set; }

        public TimeSpan TimeTo { get; set; }

        public int StoreId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Store Store { get; set; }

        #endregion

    }
}
