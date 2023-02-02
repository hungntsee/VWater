using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsInQuotationMap
        : IEntityTypeConfiguration<VWater.Data.Entities.GoodsInQuotation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.GoodsInQuotation> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods_In_Quotation", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.QuotationId)
                .IsRequired()
                .HasColumnName("Quotation_Id")
                .HasColumnType("int");

            builder.Property(t => t.GoodsId)
                .IsRequired()
                .HasColumnName("Goods_Id")
                .HasColumnType("int");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("money");

            // relationships
            builder.HasOne(t => t.Goods)
                .WithMany(t => t.GoodsInQuotations)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK_goods_in_quotation_goods");

            builder.HasOne(t => t.Quotation)
                .WithMany(t => t.GoodsInQuotations)
                .HasForeignKey(d => d.QuotationId)
                .HasConstraintName("FK_goods_in_quotation_quotation");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods_In_Quotation";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string QuotationId = "Quotation_Id";
            public const string GoodsId = "Goods_Id";
            public const string Price = "Price";
        }
        #endregion
    }
}
