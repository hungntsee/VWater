using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class ProductMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Product> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Product", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ProductName)
                .IsRequired()
                .HasColumnName("ProductName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Img)
                .IsRequired()
                .HasColumnName("Img")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ProductType_Id).IsRequired(false)
                .HasColumnName("ProductType_Id")
                .HasColumnType("int");

            builder.Property(t => t.BrandId)
                .HasColumnName("Brand_Id")
                .HasColumnType("int");

            builder.Property(t => t.IsActive)
                .HasColumnName("isActive")
                .HasColumnType("bit");

            // relationships
            builder.HasOne(t => t.ProductType)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ProductType_Id)
                .HasConstraintName("FK__Product__Product__3A4CA8FD");

            builder.HasOne(t => t.Brand)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Product_Brand");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Product";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ProductName = "ProductName";
            public const string Img = "Img";
            public const string Description = "Description";
            public const string ProductType_Id = "ProductType_Id";
            public const string BrandId = "Brand_Id";
            public const string IsActive = "isActive";
        }
        #endregion
    }
}
