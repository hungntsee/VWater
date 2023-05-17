using Microsoft.EntityFrameworkCore;
using VWater.Data.Entities;

namespace VWater.Data.Mapping
{
    public partial class WalletMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Wallet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Wallet> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Wallet", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ShipperId)
                .IsRequired()
                .HasColumnName("Shipper_Id")
                .HasColumnType("int");

            builder.Property(t => t.Credit)
                .IsRequired()
                .HasColumnName("Credit")
                .HasColumnType("money");

            // relationships
            builder.HasOne(t => t.Shipper)
                .WithOne(t => t.Wallet)
                .HasForeignKey<Wallet>(d => d.ShipperId)
                .HasConstraintName("FK_Wallet_Shipper");

            #endregion
        }
        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Wallet";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string ShipperId = "Shipper_Id";
            public const string Credit = "Credit";
        }
        #endregion
    }
}
