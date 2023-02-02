using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class ApartmentMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Apartment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Apartment> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Apartment", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ApartmentName)
                .IsRequired()
                .HasColumnName("ApartmentName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasColumnName("Area_Id")
                .HasColumnType("int");

            builder.Property(t => t.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Area)
                .WithMany(t => t.Apartments)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_apartment_area");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Apartment";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ApartmentName = "ApartmentName";
            public const string AreaId = "Area_Id";
            public const string Address = "Address";
            public const string Note = "Note";
        }
        #endregion
    }
}
