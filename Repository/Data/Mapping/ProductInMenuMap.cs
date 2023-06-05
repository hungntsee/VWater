using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class ProductInMenuMap
        : IEntityTypeConfiguration<VWater.Data.Entities.ProductInMenu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.ProductInMenu> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Product_In_Menu", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.MenuId)
                .IsRequired()
                .HasColumnName("Menu_Id")
                .HasColumnType("int");

            builder.Property(t => t.ProductId)
                .IsRequired()
                .HasColumnName("Product_Id")
                .HasColumnType("int");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("money");

            // relationships
            builder.HasOne(t => t.Product)
                .WithMany(t => t.ProductInMenus)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_product_in_menu_product");

            builder.HasOne(t => t.Menu)
                .WithMany(t => t.ProductInMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_product_in_menu_product_menu");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Product_In_Menu";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string MenuId = "Menu_Id";
            public const string ProductId = "Product_Id";
            public const string Price = "Price";
        }
        #endregion
    }
}
