using Microsoft.EntityFrameworkCore;
using VWater.Data.Entities;

namespace VWater.Data.Mapping
{
    public class DepositNoteMap
        : IEntityTypeConfiguration<VWater.Data.Entities.DepositNote>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.DepositNote> builder)
        {
            //table
            builder.ToTable("Deposit_Note", "dbo");

            //key
            builder.HasKey(t => t.Id);

            //properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.IsDeposit)
                .IsRequired()
                .HasColumnName("IsDeposit")
                .HasColumnType("bit");

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("money");

            builder.Property(t => t.OrderId)
                .IsRequired()
                .HasColumnName("Order_Id")
                .HasColumnType("int");

            //relationships

            builder.HasOne(t => t.Order)
                .WithOne(t => t.DepositNote)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<DepositNote>(t => t.OrderId)
                .HasConstraintName("FK_Deposit_Note_Order");

        }
    }
}
