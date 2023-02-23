using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class DeliveryAddressMap
        : IEntityTypeConfiguration<VWater.Data.Entities.DeliveryAddress>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.DeliveryAddress> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Delivery_Address", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("Customer_Id")
                .HasColumnType("int");

            builder.Property(t => t.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.StoreId)
                .IsRequired()
                .HasColumnName("Store_Id")
                .HasColumnType("int");

            builder.Property(t => t.BuildingId)
                .HasColumnName("Building_Id")
                .HasColumnType("int");

            builder.Property(t => t.DeliveryTypeId)
                .IsRequired()
                .HasColumnName("DeliveryType_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Building)
                .WithMany(t => t.DeliveryAddresses)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK_delivery_address_building");

            builder.HasOne(t => t.Customer)
                .WithMany(t => t.DeliveryAddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Delivery_Address_Customer");

            builder.HasOne(t => t.DeliveryType)
                .WithMany(t => t.DeliveryAddresses)
                .HasForeignKey(d => d.DeliveryTypeId)
                .HasConstraintName("FK_Delivery_Address_Delivery_Type");

            builder.HasOne(t => t.Store)
                .WithMany(t => t.DeliveryAddresses)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_delivery_address_store");

            /*builder.Navigation(a => a.Customer).AutoInclude();
            builder.Navigation(a => a.DeliveryType).AutoInclude();
            builder.Navigation(a => a.Orders).AutoInclude();
            builder.Navigation(a => a.Building).AutoInclude();*/

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Delivery_Address";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string CustomerId = "Customer_Id";
            public const string Address = "Address";
            public const string StoreId = "Store_Id";
            public const string BuildingId = "Building_Id";
            public const string DeliveryTypeId = "DeliveryType_Id";
        }
        #endregion
    }
}
