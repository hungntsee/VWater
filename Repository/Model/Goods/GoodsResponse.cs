using Azure.Core;
using Repository.Entity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Repository.Model.Goods
{
    public class GoodsResponse
    {
        [Required]
        public string GoodsName { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public string Volume { get; set; }
        [Required]
        public string Note { get; set; }
        public string BrandName { get; set; }

        public GoodsResponse(Entity.Goods goods, Brand brand, Manufacturer manufacturer)
        {
            GoodsName = goods.GoodsName;
            Img = goods.Img;
            Volume = goods.Volumne;
            Note = goods.Note;
            BrandName = brand.BrandName;
        }
    }
}
