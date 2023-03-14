using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class OrderExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Order> ByDeliveryAddressId(this IQueryable<VWater.Data.Entities.Order> queryable, int deliveryAddressId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.DeliveryAddressId == deliveryAddressId);
        }

        public static IQueryable<VWater.Data.Entities.Order> ByCustomerId(this IQueryable<VWater.Data.Entities.Order> queryable, int customerId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));
            queryable.Include(x => x.DeliveryAddress);
            return queryable.Where(q => q.DeliveryAddress.CustomerId == customerId);
        }

        public static IQueryable<VWater.Data.Entities.Order> ByDeliverySlotId(this IQueryable<VWater.Data.Entities.Order> queryable, int deliverySlotId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.DeliverySlotId == deliverySlotId);
        }

        public static VWater.Data.Entities.Order GetByKey(this IQueryable<VWater.Data.Entities.Order> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Order> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Order> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Order> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Order> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Order>(task);
        }

        public static IQueryable<VWater.Data.Entities.Order> ByStoreId(this IQueryable<VWater.Data.Entities.Order> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StoreId == storeId);
        }

        #endregion

    }
}
