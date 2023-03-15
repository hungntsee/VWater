using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class OrderMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Order> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Order", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.DeliveryAddressId)
                .IsRequired()
                .HasColumnName("DeliveryAddress_Id")
                .HasColumnType("int");

            builder.Property(t => t.StoreId)
                .IsRequired()
                .HasColumnName("Store_Id")
                .HasColumnType("int");

            builder.Property(t => t.OrderDate)
                .IsRequired()
                .HasColumnName("OrderDate")
                .HasColumnType("date");

            builder.Property(t => t.TotalQuantity)
                .IsRequired()
                .HasColumnName("TotalQuantity")
                .HasColumnType("int");

            builder.Property(t => t.TotalPrice)
                .IsRequired()
                .HasColumnName("TotalPrice")
                .HasColumnType("money");

            builder.Property(t => t.StatusId)
                .HasColumnName("Status_Id")
                .HasColumnType("int");

            builder.Property(t => t.DeliverySlotId)
                .IsRequired()
                .HasColumnName("DeliverySlot_Id")
                .HasColumnType("int");

            builder.Property(t => t.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.ShipperId)
                .HasColumnName("Shipper_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Shipper)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK__Order__Shipper_I__4F47C5E3");

            builder.HasOne(t => t.Status)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Order__Status_Id__540C7B00");

            builder.HasOne(t => t.DeliveryAddress)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.DeliveryAddressId)
                .HasConstraintName("FK_order_delivery_address");

            builder.HasOne(t => t.DeliverySlot)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.DeliverySlotId)
                .HasConstraintName("FK_order_delivery_slot");

            builder.HasOne(t => t.Store)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_order_store");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Order";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string DeliveryAddressId = "DeliveryAddress_Id";
            public const string StoreId = "Store_Id";
            public const string OrderDate = "OrderDate";
            public const string TotalQuantity = "TotalQuantity";
            public const string TotalPrice = "TotalPrice";
            public const string StatusId = "Status_Id";
            public const string DeliverySlotId = "DeliverySlot_Id";
            public const string PhoneNumber = "PhoneNumber";
            public const string ShipperId = "Shipper_Id";
        }
        #endregion
    }
}
