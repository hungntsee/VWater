using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class BuildingExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Building> ByApartmentId(this IQueryable<VWater.Data.Entities.Building> queryable, int apartmentId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ApartmentId == apartmentId);
        }

        public static VWater.Data.Entities.Building GetByKey(this IQueryable<VWater.Data.Entities.Building> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Building> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Building> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Building> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Building> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Building>(task);
        }

        #endregion

    }
}
