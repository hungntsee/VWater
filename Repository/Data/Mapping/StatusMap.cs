using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class StatusMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Status>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Status> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Status", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.StatusName)
                .IsRequired()
                .HasColumnName("StatusName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Status";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string StatusName = "StatusName";
            public const string Note = "Note";
        }
        #endregion
    }
}
