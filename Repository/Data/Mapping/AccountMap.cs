using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class AccountMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Account>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Account> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Account", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Username)
                .IsRequired()
                .HasColumnName("Username")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.Gender)
                .IsRequired()
                .HasColumnName("Gender")
                .HasColumnType("bit");

            builder.Property(t => t.DateOfBirth)
                .IsRequired()
                .HasColumnName("DateOfBirth")
                .HasColumnType("date");

            builder.Property(t => t.Address)
                .IsRequired()
                .HasColumnName("Address")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.AccessToken)
                .HasColumnName("Access_token")
                .HasColumnType("nvarchar(MAX)")
                .HasMaxLength(int.MaxValue);

            builder.Property(t => t.StoreId)
                .IsRequired(false)
                .HasColumnName("Store_Id")
                .HasColumnType("int");

            builder.Property(t => t.RoleId)
                .IsRequired()
                .HasColumnName("Role_Id")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.RoleAccountRole)
                .WithMany(t => t.RoleAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_account_account_role");

            builder.HasOne(t => t.Store)
                .WithMany(t => t.Accounts)
                .HasForeignKey(a => a.StoreId)
                .HasConstraintName("FK_Account_Store");
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Account";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string Username = "Username";
            public const string Password = "Password";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Gender = "Gender";
            public const string DateOfBirth = "DateOfBirth";
            public const string Address = "Address";
            public const string Email = "Email";
            public const string AccessToken = "Access_token";
            public const string RoleId = "Role_Id";
        }
        #endregion
    }
}
