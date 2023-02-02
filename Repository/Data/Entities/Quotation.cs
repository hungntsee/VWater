using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Quotation
    {
        public Quotation()
        {
            #region Generated Constructor
            GoodsInQuotations = new HashSet<GoodsInQuotation>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public int DistributorId { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Distributor Distributor { get; set; }

        public virtual ICollection<GoodsInQuotation> GoodsInQuotations { get; set; }

        #endregion

    }
}
