using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class DeliverySlotMap
        : IEntityTypeConfiguration<VWater.Data.Entities.DeliverySlot>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.DeliverySlot> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Delivery_Slot", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.SlotName)
                .IsRequired()
                .HasColumnName("SlotName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("date");

            builder.Property(t => t.TimeFrom)
                .IsRequired()
                .HasColumnName("TimeFrom")
                .HasColumnType("time");

            builder.Property(t => t.TimeTo)
                .IsRequired()
                .HasColumnName("TimeTo")
                .HasColumnType("time");

            builder.Property(t => t.StoreId)
                .IsRequired()
                .HasColumnName("Store_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Store)
                .WithMany(t => t.DeliverySlots)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_delivery_slot_store");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Delivery_Slot";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string SlotName = "SlotName";
            public const string Date = "Date";
            public const string TimeFrom = "TimeFrom";
            public const string TimeTo = "TimeTo";
            public const string StoreId = "Store_Id";
        }
        #endregion
    }
}
