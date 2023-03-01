using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class GoodsCompositionExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.GoodsComposition> ByGoodsId(this IQueryable<VWater.Data.Entities.GoodsComposition> queryable, int goodsId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.GoodsId == goodsId);
        }

        public static VWater.Data.Entities.GoodsComposition GetByKey(this IQueryable<VWater.Data.Entities.GoodsComposition> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsComposition> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.GoodsComposition> GetByKeyAsync(this IQueryable<VWater.Data.Entities.GoodsComposition> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsComposition> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.GoodsComposition>(task);
        }

        #endregion

    }
}
