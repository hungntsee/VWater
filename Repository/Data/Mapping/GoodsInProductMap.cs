using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsInProductMap
        : IEntityTypeConfiguration<VWater.Data.Entities.GoodsInProduct>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.GoodsInProduct> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods_In_Product", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ProductId)
                .IsRequired()
                .HasColumnName("Product_Id")
                .HasColumnType("int");

            builder.Property(t => t.GoodId)
                .IsRequired()
                .HasColumnName("Good_Id")
                .HasColumnType("int");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("money");

            // relationships
            builder.HasOne(t => t.GoodGoods)
                .WithMany(t => t.GoodGoodsInProducts)
                .HasForeignKey(d => d.GoodId)
                .HasConstraintName("FK_good_in_product_goods");

            builder.HasOne(t => t.Product)
                .WithMany(t => t.GoodsInProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_good_in_product_product");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods_In_Product";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ProductId = "Product_Id";
            public const string GoodId = "Good_Id";
            public const string Quantity = "Quantity";
            public const string Price = "Price";
        }
        #endregion
    }
}
