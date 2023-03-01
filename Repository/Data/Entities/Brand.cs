namespace VWater.Data.Entities
{
    public partial class Brand
    {
        public Brand()
        {
            #region Generated Constructor
            Goods = new HashSet<Goods>();
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string BrandName { get; set; }

        public string Logo { get; set; }

        public string Origin { get; set; }

        public int ManufactureId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Goods> Goods { get; set; }

        public virtual Manufacturer ManufactureManufacturer { get; set; }

        #endregion

    }
}
