using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class ProductTypeMap
        : IEntityTypeConfiguration<VWater.Data.Entities.ProductType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.ProductType> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Product_Type", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ProductTypeName)
                .IsRequired()
                .HasColumnName("ProductTypeName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Img)
                .IsRequired()
                .HasColumnName("Img")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Product_Type";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ProductTypeName = "ProductTypeName";
            public const string Img = "Img";
        }
        #endregion
    }
}
