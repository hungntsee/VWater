using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class GoodsExchangeNoteMap
        : IEntityTypeConfiguration<VWater.Data.Entities.GoodsExchangeNote>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.GoodsExchangeNote> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Goods_Exchange_Note", "dbo");

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

            builder.Property(t => t.WarehouseId)
                .IsRequired()
                .HasColumnName("Warehouse_Id")
                .HasColumnType("int");

            builder.Property(t => t.NoteDate)
                .IsRequired()
                .HasColumnName("NoteDate")
                .HasColumnType("date");

            builder.Property(t => t.GoodsId)
                .IsRequired()
                .HasColumnName("Goods_Id")
                .HasColumnType("int");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasColumnType("int");

            builder.Property(t => t.OrderId)
                .HasColumnName("Order_Id")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            // relationships
            builder.HasOne(t => t.Goods)
                .WithMany(t => t.GoodsExchangeNotes)
                .HasForeignKey(d => d.GoodsId)
                .HasConstraintName("FK_goods_exchange_note_goods");

            builder.HasOne(t => t.Order)
                .WithMany(t => t.GoodsExchangeNotes)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_goods_exchange_note_order");

            builder.HasOne(t => t.PurchaseOrder)
                .WithMany(t => t.GoodsExchangeNotes)
                .HasForeignKey(d => d.PurchaseOrderId)
                .HasConstraintName("FK_goods_exchange_note_purchase_order");

            builder.HasOne(t => t.Warehouse)
                .WithMany(t => t.GoodsExchangeNotes)
                .HasForeignKey(d => d.WarehouseId)
                .HasConstraintName("FK_goods_exchange_note_warehouse");

            /*builder.Navigation(a => a.Goods).AutoInclude();
            builder.Navigation(a => a.Warehouse).AutoInclude();
            builder.Navigation(a => a.Order).AutoInclude();
            builder.Navigation(a => a.PurchaseOrder).AutoInclude();*/
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Goods_Exchange_Note";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string PurchaseOrderId = "PurchaseOrder_Id";
            public const string WarehouseId = "Warehouse_Id";
            public const string NoteDate = "NoteDate";
            public const string GoodsId = "Goods_Id";
            public const string Quantity = "Quantity";
            public const string Status = "Status";
            public const string OrderId = "Order_Id";
            public const string Note = "Note";
        }
        #endregion
    }
}
