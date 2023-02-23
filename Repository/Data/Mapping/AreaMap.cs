using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class AreaMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Area>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Area> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Area", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.AreaName)
                .IsRequired()
                .HasColumnName("AreaName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.Navigation(a => a.Apartments).AutoInclude();
            builder.Navigation(a => a.Menus).AutoInclude();
            builder.Navigation(a => a.Distributors).AutoInclude();
            builder.Navigation(a => a.Stores).AutoInclude();
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Area";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string AreaName = "AreaName";
        }
        #endregion
    }
}
