using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class WarehouseBaselineMap
        : IEntityTypeConfiguration<VWater.Data.Entities.WarehouseBaseline>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.WarehouseBaseline> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Warehouse_Baseline", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.WarehouseId)
                .IsRequired()
                .HasColumnName("Warehouse_Id")
                .HasColumnType("int");

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("date");

            builder.Property(t => t.TotalQuantity)
                .IsRequired()
                .HasColumnName("TotalQuantity")
                .HasColumnType("int");

            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Warehouse)
                .WithMany(t => t.WarehouseBaselines)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK_warehouse_baseline_warehouse");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Warehouse_Baseline";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string WarehouseId = "Warehouse_Id";
            public const string Date = "Date";
            public const string TotalQuantity = "TotalQuantity";
            public const string Status = "Status";
            public const string Note = "Note";
        }
        #endregion
    }
}
