using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class WarehouseBaselineReadModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        public DateTime Date { get; set; }

        public int TotalQuantity { get; set; }

        public int? Status { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
