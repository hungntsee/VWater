using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Status
    {
        public Status()
        {
            #region Generated Constructor
            Orders = new HashSet<Order>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string StatusName { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Order> Orders { get; set; }

        #endregion

    }
}
