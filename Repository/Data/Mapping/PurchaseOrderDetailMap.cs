using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class PurchaseOrderDetailMap
        : IEntityTypeConfiguration<VWater.Data.Entities.PurchaseOrderDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.PurchaseOrderDetail> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Purchase_Order_Detail", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.PurchaseOrderId)
                .IsRequired()
                .HasColumnName("PurchaseOrder_Id")
                .HasColumnType("int");

            builder.Property(t => t.GoodsId)
                .IsRequired()
                .HasColumnName("Goods_Id")
                .HasColumnType("int");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Goods)
                .WithMany(t => t.PurchaseOrderDetails)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK_purchase_order_detail_goods");

            builder.HasOne(t => t.PurchaseOrder)
                .WithMany(t => t.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .HasConstraintName("FK_purchase_order_detail_purchase_order");
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Purchase_Order_Detail";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string PurchaseOrderId = "PurchaseOrder_Id";
            public const string GoodsId = "Goods_Id";
            public const string Quantity = "Quantity";
        }
        #endregion
    }
}
