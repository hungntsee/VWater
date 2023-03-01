using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class ManufacturerExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Manufacturer GetByKey(this IQueryable<VWater.Data.Entities.Manufacturer> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Manufacturer> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Manufacturer> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Manufacturer> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Manufacturer> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Manufacturer>(task);
        }

        #endregion

    }
}
