using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class WarehouseMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Warehouse>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Warehouse> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Warehouse", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.WarehouseName)
                .IsRequired()
                .HasColumnName("WarehouseName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.StoreId)
                .IsRequired()
                .HasColumnName("Store_Id")
                .HasColumnType("int");

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasColumnName("Area_Id")
                .HasColumnType("int");

            builder.Property(t => t.Capacity)
                .HasColumnName("Capacity")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            // relationships
            builder.HasOne(t => t.Area)
                .WithMany(t => t.Warehouses)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_warehouse_area");

            builder.HasOne(t => t.Store)
                .WithMany(t => t.Warehouses)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_warehouse_store");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Warehouse";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string WarehouseName = "WarehouseName";
            public const string StoreId = "Store_Id";
            public const string AreaId = "Area_Id";
            public const string Capacity = "Capacity";
            public const string PhoneNumber = "PhoneNumber";
        }
        #endregion
    }
}
