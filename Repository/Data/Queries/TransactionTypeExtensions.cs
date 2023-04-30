using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class TransactionTypeExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.TransactionType GetByKey(this IQueryable<VWater.Data.Entities.TransactionType> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.TransactionType> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.TransactionType> GetByKeyAsync(this IQueryable<VWater.Data.Entities.TransactionType> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.TransactionType> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.TransactionType>(task);
        }

        #endregion

    }
}
