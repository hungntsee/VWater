using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class MenuMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Menu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Menu> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Menu", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasColumnName("Area_Id")
                .HasColumnType("int");

            builder.Property(t => t.ValidFrom)
                .IsRequired()
                .HasColumnName("ValidFrom")
                .HasColumnType("date");

            builder.Property(t => t.ValidTo)
                .IsRequired()
                .HasColumnName("ValidTo")
                .HasColumnType("date");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Area)
                .WithMany(t => t.Menus)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_product_menu_area");

            /*builder.Navigation(a => a.ProductInMenus).AutoInclude();
            builder.Navigation(a => a.Area).AutoInclude();*/
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Menu";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string AreaId = "Area_Id";
            public const string ValidFrom = "ValidFrom";
            public const string ValidTo = "ValidTo";
            public const string Note = "Note";
        }
        #endregion
    }
}
