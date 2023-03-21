using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsInProductMap
        : IEntityTypeConfiguration<VWater.Data.Entities.GoodsInProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.GoodsInProduct> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods_In_Product", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ProductId)
                .IsRequired()
                .HasColumnName("Product_Id")
                .HasColumnType("int");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("money");

            builder.Property(t => t.GoodsInBaselineId)
                .HasColumnName("GoodsInBaseline_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Product)
                .WithMany(t => t.GoodsInProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_good_in_product_product");

            builder.HasOne(t => t.GoodsInBaseline)
                .WithMany(t => t.GoodsInProducts)
                .HasForeignKey(d => d.GoodsInBaselineId)
                .HasConstraintName("FK_Goods_In_Product_Goods_In_Baseline");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods_In_Product";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ProductId = "Product_Id";
            public const string Quantity = "Quantity";
            public const string Price = "Price";
            public const string GoodsInBaselineId = "GoodsInBaseline_Id";
        }
        #endregion
    }
}
