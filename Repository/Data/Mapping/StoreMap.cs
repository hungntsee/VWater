using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class StoreMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Store>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Store> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Store", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.StoreName)
                .IsRequired()
                .HasColumnName("StoreName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasColumnName("Area_Id")
                .HasColumnType("int");

            builder.Property(t => t.Address)
                .HasColumnName("Address")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Area)
                .WithMany(t => t.Stores)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_store_area");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Store";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string StoreName = "StoreName";
            public const string AreaId = "Area_Id";
            public const string Address = "Address";
            public const string Status = "Status";
            public const string Note = "Note";
        }
        #endregion
    }
}