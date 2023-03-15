using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class CustomerMap
        : IEntityTypeConfiguration<VWater.Data.Entities.Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.Customer> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Customer", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasColumnName("FullName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nchar(10)")
                .HasMaxLength(10);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Customer";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string FullName = "FullName";
            public const string PhoneNumber = "PhoneNumber";
            public const string Note = "Note";
        }
        #endregion
    }
}
