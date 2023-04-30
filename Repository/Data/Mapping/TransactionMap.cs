using Microsoft.EntityFrameworkCore;
using VWater.Data.Entities;

namespace VWater.Data.Mapping
{
    public partial class TransactionMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Transaction>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Transaction> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Transaction", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("datetime");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("money");

            builder.Property(t => t.WalletId)
                .IsRequired()
                .HasColumnName("Wallet_Id")
                .HasColumnType("int");

            builder.Property(t => t.OrderId)
                .IsRequired(false)
                .HasColumnName("Order_Id")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .IsRequired(false)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.AccountId)
                .IsRequired()
                .HasColumnName("Account_Id")
                .HasColumnType("int");

            builder.Property(t => t.TransactionType_Id).IsRequired(false)
                .HasColumnName("TransactionType_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.Order)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Transaction_Order");

            builder.HasOne(t => t.Wallet)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.WalletId)
                .HasConstraintName("FK_Transaction_Wallet")
                .IsRequired();

            builder.HasOne(t => t.Account)
                .WithMany(t => t.Transaction)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Transaction_Account");

            builder.HasOne(t => t.TransactionType)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Transaction_Transaction_Type");
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Transaction";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string Date = "Date";
            public const string Price = "Price";
            public const string WalletId = "Wallet_Id";
            public const string OrderId = "Order_Id";
            public const string Note = "Note";
            public const string AccountId = "Account_Id";
            public const string TransactionTypeId = "TransactionType_Id";
        }
        #endregion
    }
}

