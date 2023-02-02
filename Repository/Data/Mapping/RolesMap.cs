using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class RolesMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Roles>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Roles> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Roles", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.RoleName)
                .IsRequired()
                .HasColumnName("RoleName")
                .HasColumnType("nvarchar(max)");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Roles";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string RoleName = "RoleName";
        }
        #endregion
    }
}
