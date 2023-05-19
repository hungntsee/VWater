using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class ProductExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Product> ByBrandId(this IQueryable<VWater.Data.Entities.Product> queryable, int? brandId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.BrandId == brandId || (brandId == null && q.BrandId == null)));
        }

        public static VWater.Data.Entities.Product GetByKey(this IQueryable<VWater.Data.Entities.Product> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Product> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Product> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Product> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Product> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Product>(task);
        }

        public static IQueryable<VWater.Data.Entities.Product> ByProductTypeId(this IQueryable<VWater.Data.Entities.Product> queryable, int? productTypeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.ProductType_Id == productTypeId || (productTypeId == null && q.ProductType_Id == null)));
        }

        public static IQueryable<VWater.Data.Entities.Product> ByProductName(this IQueryable<VWater.Data.Entities.Product> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.ProductName.Contains(k)));
            return queryable.Where(x => x.ProductName.Trim().ToLower().Contains(search.Trim().ToLower()));
        }

        public static IQueryable<VWater.Data.Entities.Product> ByProductNameVoi(this IQueryable<VWater.Data.Entities.Product> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.ProductName.Contains(k)));
            return queryable.Where(x => x.ProductName.Trim().ToLower().Contains(search.Trim().ToLower()) && x.ProductType_Id==1);
        }

        public static IQueryable<VWater.Data.Entities.Product> ByProductNameUp(this IQueryable<VWater.Data.Entities.Product> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.ProductName.Contains(k)));
            return queryable.Where(x => x.ProductName.Trim().ToLower().Contains(search.Trim().ToLower()) && x.ProductType_Id == 2);
        }

        public static IQueryable<VWater.Data.Entities.Product> ByProductName350(this IQueryable<VWater.Data.Entities.Product> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.ProductName.Contains(k)));
            return queryable.Where(x => x.ProductName.Trim().ToLower().Contains(search.Trim().ToLower()) && x.ProductType_Id == 3);
        }

        public static IQueryable<VWater.Data.Entities.Product> ByProductName550(this IQueryable<VWater.Data.Entities.Product> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.ProductName.Contains(k)));
            return queryable.Where(x => x.ProductName.Trim().ToLower().Contains(search.Trim().ToLower()) && x.ProductType_Id == 4);
        }
        #endregion

    }
}
