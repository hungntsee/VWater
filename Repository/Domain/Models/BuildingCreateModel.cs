using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class BuildingCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string BuildingName { get; set; }

        public int ApartmentId { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
