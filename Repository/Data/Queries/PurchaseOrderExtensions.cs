using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class PurchaseOrderExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.PurchaseOrder> ByDistributorId(this IQueryable<VWater.Data.Entities.PurchaseOrder> queryable, int distributorId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.DistributorId == distributorId);
        }

        public static VWater.Data.Entities.PurchaseOrder GetByKey(this IQueryable<VWater.Data.Entities.PurchaseOrder> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.PurchaseOrder> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.PurchaseOrder> GetByKeyAsync(this IQueryable<VWater.Data.Entities.PurchaseOrder> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.PurchaseOrder> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.PurchaseOrder>(task);
        }

        public static IQueryable<VWater.Data.Entities.PurchaseOrder> ByStoreId(this IQueryable<VWater.Data.Entities.PurchaseOrder> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StoreId == storeId);
        }

        #endregion

    }
}
