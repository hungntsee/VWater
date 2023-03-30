using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class SysdiagramsUpdateModel
    {
        #region Generated Properties
        public string Name { get; set; }

        public int PrincipalId { get; set; }

        public int DiagramId { get; set; }

        public int? Version { get; set; }

        public Byte[] Definition { get; set; }

        #endregion

    }
}
