using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Shipper
    {
        public Shipper()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Fullname { get; set; }

        public string PhoneNumber { get; set; }

        public int StoreId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Account Account { get; set; }

        public virtual Store Store { get; set; }

        #endregion

    }
}
