using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class BrandMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Brand>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Brand> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Brand", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.BrandName)
                .IsRequired()
                .HasColumnName("BrandName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Logo)
                .HasColumnName("Logo")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Origin)
                .HasColumnName("Origin")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ManufactureId)
                .IsRequired()
                .HasColumnName("Manufacture_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.ManufactureManufacturer)
                .WithMany(t => t.ManufactureBrands)
                .HasForeignKey(d => d.ManufactureId)
                .HasConstraintName("FK_brand_manufacture");

            /*builder.Navigation(a => a.ManufactureManufacturer).AutoInclude();
            builder.Navigation(a => a.Goods).AutoInclude();*/
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Brand";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string BrandName = "BrandName";
            public const string Logo = "Logo";
            public const string Origin = "Origin";
            public const string ManufactureId = "Manufacture_Id";
        }
        #endregion
    }
}
