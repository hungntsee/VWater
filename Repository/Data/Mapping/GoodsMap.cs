using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Goods>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Goods> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.GoodsName)
                .IsRequired()
                .HasColumnName("GoodsName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Img)
                .HasColumnName("Img")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Volume)
                .HasColumnName("Volume")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.BrandId)
                .IsRequired()
                .HasColumnName("Brand_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Brand)
                .WithMany(t => t.Goods)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_goods_brand1");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string GoodsName = "GoodsName";
            public const string Img = "Img";
            public const string Volume = "Volume";
            public const string Note = "Note";
            public const string BrandId = "Brand_Id";
        }
        #endregion
    }
}
