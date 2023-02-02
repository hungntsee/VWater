using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class WarehouseReadModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public int StoreId { get; set; }

        public int AreaId { get; set; }

        public string Capacity { get; set; }

        public string PhoneNumber { get; set; }

        #endregion

    }
}
