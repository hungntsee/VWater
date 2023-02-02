using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class DeliveryAddressReadModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; }

        public int StoreId { get; set; }

        public int? BuildingId { get; set; }

        public int DeliveryTypeId { get; set; }

        #endregion

    }
}
