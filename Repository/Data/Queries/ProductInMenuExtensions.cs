using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class ProductInMenuExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.ProductInMenu GetByKey(this IQueryable<VWater.Data.Entities.ProductInMenu> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.ProductInMenu> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.ProductInMenu> GetByKeyAsync(this IQueryable<VWater.Data.Entities.ProductInMenu> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.ProductInMenu> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.ProductInMenu>(task);
        }

        public static IQueryable<VWater.Data.Entities.ProductInMenu> ByMenuId(this IQueryable<VWater.Data.Entities.ProductInMenu> queryable, int menuId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.MenuId == menuId && q.Product.IsActive==true);
        }

        public static IQueryable<VWater.Data.Entities.ProductInMenu> ByProductId(this IQueryable<VWater.Data.Entities.ProductInMenu> queryable, int productId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ProductId == productId);
        }

        #endregion

    }
}
