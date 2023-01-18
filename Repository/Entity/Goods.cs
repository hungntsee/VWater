namespace Repository.Entity
{
    public class Goods
    {
        public int Id { get; set; }
        public string GoodsName { get; set; }
        public string Img { get; set; }
        public string Volumne { get; set; }
        public string Note { get; set; }
        public int? Brand_Id { get; set; }
        public virtual Brand BrandVirtutal { get; set; }
    }
}
