using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class DistributorMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Distributor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Distributor> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Distributor", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.DistributorName)
                .IsRequired()
                .HasColumnName("DistributorName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.StartDate)
                .IsRequired()
                .HasColumnName("StartDate")
                .HasColumnType("date");

            builder.Property(t => t.EndDate)
                .IsRequired()
                .HasColumnName("EndDate")
                .HasColumnType("date");

            builder.Property(t => t.Payment)
                .IsRequired()
                .HasColumnName("Payment")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasColumnName("Area_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Area)
                .WithMany(t => t.Distributors)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_distributor_area");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Distributor";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string DistributorName = "DistributorName";
            public const string PhoneNumber = "PhoneNumber";
            public const string Address = "Address";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string Payment = "Payment";
            public const string AreaId = "Area_Id";
        }
        #endregion
    }
}
