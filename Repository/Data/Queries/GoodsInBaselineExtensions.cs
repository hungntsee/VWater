using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class GoodsInBaselineExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.GoodsInBaseline> ByGoodsId(this IQueryable<VWater.Data.Entities.GoodsInBaseline> queryable, int goodsId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.GoodsId == goodsId);
        }

        public static VWater.Data.Entities.GoodsInBaseline GetByKey(this IQueryable<VWater.Data.Entities.GoodsInBaseline> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsInBaseline> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.GoodsInBaseline> GetByKeyAsync(this IQueryable<VWater.Data.Entities.GoodsInBaseline> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsInBaseline> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.GoodsInBaseline>(task);
        }

        public static IQueryable<VWater.Data.Entities.GoodsInBaseline> ByWarehouseBaselineId(this IQueryable<VWater.Data.Entities.GoodsInBaseline> queryable, int warehouseBaselineId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.WarehouseBaselineId == warehouseBaselineId);
        }

        #endregion

    }
}
