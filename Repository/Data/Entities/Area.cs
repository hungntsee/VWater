using System;
using System.Collections.Generic;

namespace VWater.Data.Entities
{
    public partial class Area
    {
        public Area()
        {
            #region Generated Constructor
            Apartments = new HashSet<Apartment>();
            Distributors = new HashSet<Distributor>();
            Menus = new HashSet<Menu>();
            Stores = new HashSet<Store>();
            Warehouses = new HashSet<Warehouse>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string AreaName { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Apartment> Apartments { get; set; }

        public virtual ICollection<Distributor> Distributors { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }

        #endregion

    }
}
