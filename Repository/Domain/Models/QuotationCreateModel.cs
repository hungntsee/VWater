using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class QuotationCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public int DistributorId { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
