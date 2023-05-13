using Microsoft.EntityFrameworkCore;
using VWater.Data.Entities;

namespace VWater.Data.Mapping
{
    public partial class ShipperMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Shipper>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Shipper> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Shipper", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.AccountId)
                .IsRequired()
                .HasColumnName("Account_Id")
                .HasColumnType("int");

            builder.Property(t => t.Fullname)
                .IsRequired()
                .HasColumnName("Fullname")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.IsOnline)
                .IsRequired(false)
                .HasColumnName("IsOnline")
                .HasColumnType("bit");

            builder.Property(t => t.IsActive)
                .HasColumnName("isActive")
                .HasColumnType("bit");

            // relationships
            builder.HasOne(t => t.Account)
                .WithOne(t => t.Shipper)
                .HasForeignKey<Shipper>(d => d.AccountId)
                .HasConstraintName("FK_shipper_account")
                .OnDelete(DeleteBehavior.Cascade);


            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Shipper";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string AccountId = "Account_Id";
            public const string Fullname = "Fullname";
            public const string PhoneNumber = "PhoneNumber";
            public const string IsOnline = "isOnline";
            public const string IsActive = "isActive";
        }
        #endregion
    }
}
