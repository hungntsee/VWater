using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Distributor
    {
        public Distributor()
        {
            #region Generated Constructor
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Quotations = new HashSet<Quotation>();
            #endregion
        }

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

        #region Generated Relationships
        public virtual Area Area { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        public virtual ICollection<Quotation> Quotations { get; set; }

        #endregion

    }
}
