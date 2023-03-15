using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class OrderDetailMap
        : IEntityTypeConfiguration<VWater.Data.Entities.OrderDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.OrderDetail> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Order_Detail", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.OrderId)
                .IsRequired()
                .HasColumnName("Order_Id")
                .HasColumnType("int");

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

            // relationships
            builder.HasOne(t => t.Order)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_detail_order");

            builder.HasOne(t => t.Product)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_order_detail_product");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Order_Detail";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string OrderId = "Order_Id";
            public const string ProductId = "Product_Id";
            public const string Quantity = "Quantity";
            public const string Price = "Price";
        }
        #endregion
    }
}
