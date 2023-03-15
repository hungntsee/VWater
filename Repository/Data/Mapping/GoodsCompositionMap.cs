using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsCompositionMap
        : IEntityTypeConfiguration<VWater.Data.Entities.GoodsComposition>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.GoodsComposition> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods_Composition", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Img)
                .HasColumnName("Img")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Volume)
                .HasColumnName("Volume")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.GoodsId)
                .IsRequired()
                .HasColumnName("Goods_Id")
                .HasColumnType("int");

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Goods)
                .WithMany(t => t.GoodsCompositions)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK_goods_composition_goods");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods_Composition";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Img = "Img";
            public const string Volume = "Volume";
            public const string GoodsId = "Goods_Id";
            public const string Description = "Description";
        }
        #endregion
    }
}
