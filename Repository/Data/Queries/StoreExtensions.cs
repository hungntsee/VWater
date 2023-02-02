using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class StoreExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Store> ByAreaId(this IQueryable<VWater.Data.Entities.Store> queryable, int areaId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.AreaId == areaId);
        }

        public static VWater.Data.Entities.Store GetByKey(this IQueryable<VWater.Data.Entities.Store> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Store> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Store> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Store> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Store> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Store>(task);
        }

        #endregion

    }
}
