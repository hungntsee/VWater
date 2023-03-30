using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class SysdiagramsMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Sysdiagrams>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Sysdiagrams> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("sysdiagrams", "dbo");

            // key
            builder.HasKey(t => t.DiagramId);

            // properties
            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("nvarchar(128)")
                .HasMaxLength(128);

            builder.Property(t => t.PrincipalId)
                .IsRequired()
                .HasColumnName("principal_id")
                .HasColumnType("int");

            builder.Property(t => t.DiagramId)
                .IsRequired()
                .HasColumnName("diagram_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Version)
                .HasColumnName("version")
                .HasColumnType("int");

            builder.Property(t => t.Definition)
                .HasColumnName("definition")
                .HasColumnType("varbinary(max)");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "sysdiagrams";
        }

        public struct Columns
        {
            public const string Name = "name";
            public const string PrincipalId = "principal_id";
            public const string DiagramId = "diagram_id";
            public const string Version = "version";
            public const string Definition = "definition";
        }
        #endregion
    }
}
