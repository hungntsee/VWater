using System;
using System.Collections.Generic;

namespace VWater.Domain.Models
{
    public partial class GoodsCompositionReadModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public string Volume { get; set; }

        public int GoodsId { get; set; }

        public string Description { get; set; }

        #endregion

    }
}
