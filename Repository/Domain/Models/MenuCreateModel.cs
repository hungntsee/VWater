using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class MenuCreateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int AreaId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Note { get; set; }

        #endregion

    }
}
