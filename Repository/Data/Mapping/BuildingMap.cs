using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class BuildingMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Building>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Building> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Building", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.BuildingName)
                .IsRequired()
                .HasColumnName("BuildingName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.ApartmentId)
                .IsRequired()
                .HasColumnName("Apartment_Id")
                .HasColumnType("int");

            builder.Property(t => t.Address)
                .HasColumnName("Address")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Apartment)
                .WithMany(t => t.Buildings)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("FK_building_apartment");

            builder.Navigation(a => a.Apartment).AutoInclude();
            builder.Navigation(a => a.DeliveryAddresses).AutoInclude();
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Building";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string BuildingName = "BuildingName";
            public const string ApartmentId = "Apartment_Id";
            public const string Address = "Address";
            public const string Note = "Note";
        }
        #endregion
    }
}
