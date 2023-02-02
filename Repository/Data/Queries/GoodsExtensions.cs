using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class GoodsExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Goods> ByBrandId(this IQueryable<VWater.Data.Entities.Goods> queryable, int brandId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.BrandId == brandId);
        }

        public static VWater.Data.Entities.Goods GetByKey(this IQueryable<VWater.Data.Entities.Goods> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Goods> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Goods> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Goods> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Goods> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Goods>(task);
        }

        #endregion

    }
}
