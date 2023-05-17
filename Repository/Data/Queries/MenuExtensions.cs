using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class MenuExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Menu> ByStoreId(this IQueryable<VWater.Data.Entities.Menu> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StoreId == storeId);
        }

        public static VWater.Data.Entities.Menu GetByKey(this IQueryable<VWater.Data.Entities.Menu> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Menu> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Menu> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Menu> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Menu> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Menu>(task);
        }

        #endregion

    }
}
