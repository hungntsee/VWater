namespace VWater.Domain.Models
{
    public partial class MenuCreateModel
    {
        #region Generated Properties


        public int AreaId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Note { get; set; }

        public ICollection<ProductInMenuCreateModel> ProductInMenus { get; set; }

        #endregion

    }
}
