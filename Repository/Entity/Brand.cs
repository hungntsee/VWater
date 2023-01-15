namespace Repository.Entity
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public string Origin { get; set; }
        public int? Manufacturer_Id { get; set; }

        public virtual ICollection<Goods> Goodss { get; private set; }
        public virtual Manufacturer ManufacturerVirtual { get; set; }
    }
}
