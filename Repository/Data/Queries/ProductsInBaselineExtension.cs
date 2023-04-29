using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class ProductsInBaselineExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.ProductsInBaseline> ByProductId(this IQueryable<VWater.Data.Entities.ProductsInBaseline> queryable, int productId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ProductId == productId);
        }

        public static VWater.Data.Entities.ProductsInBaseline GetByKey(this IQueryable<VWater.Data.Entities.ProductsInBaseline> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.ProductsInBaseline> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.ProductsInBaseline> GetByKeyAsync(this IQueryable<VWater.Data.Entities.ProductsInBaseline> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.ProductsInBaseline> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.ProductsInBaseline>(task);
        }

        public static IQueryable<VWater.Data.Entities.ProductsInBaseline> ByWarehouseBaselineId(this IQueryable<VWater.Data.Entities.ProductsInBaseline> queryable, int warehouseBaselineId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.WarehouseBaselineId == warehouseBaselineId);
        }

        #endregion

    }
}
