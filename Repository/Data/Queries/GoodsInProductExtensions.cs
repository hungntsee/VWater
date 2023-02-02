using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class GoodsInProductExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.GoodsInProduct> ByGoodId(this IQueryable<VWater.Data.Entities.GoodsInProduct> queryable, int goodId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.GoodId == goodId);
        }

        public static VWater.Data.Entities.GoodsInProduct GetByKey(this IQueryable<VWater.Data.Entities.GoodsInProduct> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsInProduct> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.GoodsInProduct> GetByKeyAsync(this IQueryable<VWater.Data.Entities.GoodsInProduct> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsInProduct> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.GoodsInProduct>(task);
        }

        public static IQueryable<VWater.Data.Entities.GoodsInProduct> ByProductId(this IQueryable<VWater.Data.Entities.GoodsInProduct> queryable, int productId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ProductId == productId);
        }

        #endregion

    }
}
