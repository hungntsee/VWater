using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class AccountRoleMap
        : IEntityTypeConfiguration<VWater.Data.Entities.AccountRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.AccountRole> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Account_Role", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.RoleName)
                .IsRequired()
                .HasColumnName("RoleName")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Account_Role";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string RoleName = "RoleName";
        }
        #endregion
    }
}
