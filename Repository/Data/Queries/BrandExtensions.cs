using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class BrandExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Brand GetByKey(this IQueryable<VWater.Data.Entities.Brand> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Brand> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Brand> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Brand> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Brand> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Brand>(task);
        }

        public static IQueryable<VWater.Data.Entities.Brand> ByManufactureId(this IQueryable<VWater.Data.Entities.Brand> queryable, int manufactureId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ManufactureId == manufactureId);
        }

        #endregion

    }
}
