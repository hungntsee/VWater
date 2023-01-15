using System.ComponentModel.DataAnnotations;

namespace Repository.Model.Goods
{
    public class GoodsUpdateRequest
    {
        public int Id { get; set; }
        public string GoodsName { get; set; }
        public string Img { get; set; }
        public string Volume { get; set; }
        public string Note { get; set; }
        public string BrandName { get; set; }

        private string replaceEmptyWithNull(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
