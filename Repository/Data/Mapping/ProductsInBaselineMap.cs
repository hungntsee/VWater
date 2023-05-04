using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class ProductsInBaselineMap
       : IEntityTypeConfiguration<VWater.Data.Entities.ProductsInBaseline>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.ProductsInBaseline> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Products_In_Baseline", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.WarehouseBaselineId)
                .IsRequired()
                .HasColumnName("WarehouseBaseline_Id")
                .HasColumnType("int");

            builder.Property(t => t.ProductId)
                .IsRequired()
                .HasColumnName("Product_Id")
                .HasColumnType("int");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .IsRequired(false)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Product)
                .WithMany(t => t.ProductsInBaselines)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Products_In_Baseline_Product");

            builder.HasOne(t => t.WarehouseBaseline)
                .WithMany(t => t.ProductsInBaselines)
                .HasForeignKey(d => d.WarehouseBaselineId)
                .HasConstraintName("FK_Products_In_Baseline_Warehouse_Baseline");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Products_In_Baseline";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string WarehouseBaselineId = "WarehouseBaseline_Id";
            public const string ProductsId = "Products_Id";
            public const string Quantity = "Quantity";
            public const string Note = "Note";
        }
        #endregion
    }
}
