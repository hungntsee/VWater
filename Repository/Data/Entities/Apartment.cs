namespace VWater.Data.Entities
{
    public partial class Apartment
    {
        public Apartment()
        {
            #region Generated Constructor
            Buildings = new HashSet<Building>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string ApartmentName { get; set; }

        public int AreaId { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Area Area { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }

        #endregion

    }
}
