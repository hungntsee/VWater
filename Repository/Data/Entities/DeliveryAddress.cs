using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class DeliveryAddress
    {
        public DeliveryAddress()
        {
            #region Generated Constructor
            Orders = new HashSet<Order>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; }

        public int StoreId { get; set; }

        public int? BuildingId { get; set; }

        public int DeliveryTypeId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Building Building { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DeliveryType DeliveryType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual Store Store { get; set; }

        #endregion

    }
}
