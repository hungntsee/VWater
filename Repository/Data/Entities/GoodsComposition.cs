namespace VWater.Data.Entities
{
    public partial class GoodsComposition
    {
        public GoodsComposition()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public string Volume { get; set; }

        public int GoodsId { get; set; }

        public string Description { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Goods Goods { get; set; }

        #endregion

    }
}
