using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class DistributorUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string DistributorName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Payment { get; set; }

        public int AreaId { get; set; }

        #endregion

    }
}
