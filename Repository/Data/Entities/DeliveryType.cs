using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            #region Generated Constructor
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string TypeName { get; set; }

        public int TypeLevel { get; set; }

        public string Description { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        #endregion

    }
}
