using System;
using System.Collections.Generic;
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

            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasColumnType("int");

            builder.Property(t => t.DeliverySlotId)
                .IsRequired()
                .HasColumnName("DeliverySlot_Id")
                .HasColumnType("int");

            // relationships
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
            public const string Status = "Status";
            public const string DeliverySlotId = "DeliverySlot_Id";
        }
        #endregion
    }
}