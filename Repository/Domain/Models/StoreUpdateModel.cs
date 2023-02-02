using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class StoreUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string StoreName { get; set; }

        public int AreaId { get; set; }

        public string Address { get; set; }

        public int? Status { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
