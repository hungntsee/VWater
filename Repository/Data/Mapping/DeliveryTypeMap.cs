using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class DeliveryTypeMap
        : IEntityTypeConfiguration<VWater.Data.Entities.DeliveryType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.DeliveryType> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Delivery_Type", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.TypeName)
                .IsRequired()
                .HasColumnName("TypeName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.TypeLevel)
                .IsRequired()
                .HasColumnName("TypeLevel")
                .HasColumnType("int");

            builder.Property(t => t.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.Navigation(a => a.DeliveryAddresses).AutoInclude();
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Delivery_Type";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string TypeName = "TypeName";
            public const string TypeLevel = "TypeLevel";
            public const string Description = "Description";
        }
        #endregion
    }
}
