using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Goods
{
    public class GoodsRequest
    {
        [Required]
        public string GoodsName { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public string Volume { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public int? Brand_Id { get; set; }
    }
}
