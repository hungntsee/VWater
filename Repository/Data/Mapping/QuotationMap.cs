using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class QuotationMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Quotation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Quotation> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Quotation", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ValidFrom)
                .IsRequired()
                .HasColumnName("ValidFrom")
                .HasColumnType("date");

            builder.Property(t => t.ValidTo)
                .HasColumnName("ValidTo")
                .HasColumnType("date");

            builder.Property(t => t.DistributorId)
                .IsRequired()
                .HasColumnName("Distributor_Id")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Distributor)
                .WithMany(t => t.Quotations)
                .HasForeignKey(d => d.DistributorId)
                .HasConstraintName("FK_quotation_distributor");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Quotation";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ValidFrom = "ValidFrom";
            public const string ValidTo = "ValidTo";
            public const string DistributorId = "Distributor_Id";
            public const string Note = "Note";
        }
        #endregion
    }
}
