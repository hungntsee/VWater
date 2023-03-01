using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class DistributorExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Distributor> ByAreaId(this IQueryable<VWater.Data.Entities.Distributor> queryable, int areaId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.AreaId == areaId);
        }

        public static VWater.Data.Entities.Distributor GetByKey(this IQueryable<VWater.Data.Entities.Distributor> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Distributor> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Distributor> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Distributor> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Distributor> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Distributor>(task);
        }

        #endregion

    }
}
