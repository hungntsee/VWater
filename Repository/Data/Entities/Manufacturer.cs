using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            #region Generated Constructor
            ManufactureBrands = new HashSet<Brand>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string ManufacturerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Brand> ManufactureBrands { get; set; }

        #endregion

    }
}
