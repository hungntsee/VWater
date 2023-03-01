using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class WarehouseBaselineExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.WarehouseBaseline GetByKey(this IQueryable<VWater.Data.Entities.WarehouseBaseline> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.WarehouseBaseline> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.WarehouseBaseline> GetByKeyAsync(this IQueryable<VWater.Data.Entities.WarehouseBaseline> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.WarehouseBaseline> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.WarehouseBaseline>(task);
        }

        public static IQueryable<VWater.Data.Entities.WarehouseBaseline> ByWarehouseId(this IQueryable<VWater.Data.Entities.WarehouseBaseline> queryable, int warehouseId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.WarehouseId == warehouseId);
        }

        #endregion

    }
}
