using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class CustomerMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Customer> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Customer", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasColumnName("FullName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);

            // relationships

            builder.Navigation(a => a.DeliveryAddresses).AutoInclude();
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Customer";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string FullName = "FullName";
            public const string Password = "PhoneNumber";
            public const string Note = "Note";
        }
        #endregion
    }
}
