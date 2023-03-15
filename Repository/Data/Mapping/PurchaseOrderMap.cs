using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class PurchaseOrderMap
        : IEntityTypeConfiguration<VWater.Data.Entities.PurchaseOrder>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.PurchaseOrder> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Purchase_Order", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.StoreId)
                .IsRequired()
                .HasColumnName("Store_Id")
                .HasColumnType("int");

            builder.Property(t => t.DistributorId)
                .IsRequired()
                .HasColumnName("Distributor_Id")
                .HasColumnType("int");

            builder.Property(t => t.OrderDate)
                .IsRequired()
                .HasColumnName("OrderDate")
                .HasColumnType("date");

            builder.Property(t => t.TotalQuantity)
                .IsRequired()
                .HasColumnName("TotalQuantity")
                .HasColumnType("int");

            builder.Property(t => t.ToatalPrice)
                .IsRequired()
                .HasColumnName("ToatalPrice")
                .HasColumnType("money");

            builder.Property(t => t.StatusId)
                .HasColumnName("Status_Id")
                .HasColumnType("int");

            builder.Property(t => t.Payment)
                .HasColumnName("Payment")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Status)
                .WithMany(t => t.PurchaseOrders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Purchase___Statu__55009F39");

            builder.HasOne(t => t.Distributor)
                .WithMany(t => t.PurchaseOrders)
                .HasForeignKey(d => d.DistributorId)
                .HasConstraintName("FK_purchase_order_distributor");

            builder.HasOne(t => t.Store)
                .WithMany(t => t.PurchaseOrders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_purchase_order_store");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Purchase_Order";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string StoreId = "Store_Id";
            public const string DistributorId = "Distributor_Id";
            public const string OrderDate = "OrderDate";
            public const string TotalQuantity = "TotalQuantity";
            public const string ToatalPrice = "ToatalPrice";
            public const string StatusId = "Status_Id";
            public const string Payment = "Payment";
            public const string Note = "Note";
        }
        #endregion
    }
}
