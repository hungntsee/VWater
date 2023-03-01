using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsInBaselineMap
        : IEntityTypeConfiguration<VWater.Data.Entities.GoodsInBaseline>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.GoodsInBaseline> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods_In_Baseline", "dbo");

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

            builder.Property(t => t.GoodsId)
                .IsRequired()
                .HasColumnName("Goods_Id")
                .HasColumnType("int");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Goods)
                .WithMany(t => t.GoodsInBaselines)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK_goods_in_baseline_goods");

            builder.HasOne(t => t.WarehouseBaseline)
                .WithMany(t => t.GoodsInBaselines)
                .HasForeignKey(d => d.WarehouseBaselineId)
                .HasConstraintName("FK_goods_in_baseline_warehouse_baseline");

            /*builder.Navigation(a => a.Goods).AutoInclude();
            builder.Navigation(a => a.WarehouseBaseline).AutoInclude();*/

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods_In_Baseline";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string WarehouseBaselineId = "WarehouseBaseline_Id";
            public const string GoodsId = "Goods_Id";
            public const string Quantity = "Quantity";
            public const string Note = "Note";
        }
        #endregion
    }
}
