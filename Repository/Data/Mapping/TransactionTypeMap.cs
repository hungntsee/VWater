using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Mapping
{
    public partial class TransactionTypeMap
        : IEntityTypeConfiguration<VWater.Data.Entities.TransactionType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VWater.Data.Entities.TransactionType> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Transaction_Type", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.TransactionTypeName)
                .IsRequired()
                .HasColumnName("TransactionTypeName")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Transaction_Type";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string TransactionTypeName = "TransactionTypeName";
        }
        #endregion
    }
}
