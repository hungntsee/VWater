using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class WarehouseExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Warehouse GetByKey(this IQueryable<VWater.Data.Entities.Warehouse> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Warehouse> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Warehouse> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Warehouse> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Warehouse> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Warehouse>(task);
        }

        public static IQueryable<VWater.Data.Entities.Warehouse> ByStoreId(this IQueryable<VWater.Data.Entities.Warehouse> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StoreId == storeId);
        }

        #endregion

    }
}
