using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class DeliverySlotExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.DeliverySlot GetByKey(this IQueryable<VWater.Data.Entities.DeliverySlot> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.DeliverySlot> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.DeliverySlot> GetByKeyAsync(this IQueryable<VWater.Data.Entities.DeliverySlot> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.DeliverySlot> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.DeliverySlot>(task);
        }

        public static IQueryable<VWater.Data.Entities.DeliverySlot> ByStoreId(this IQueryable<VWater.Data.Entities.DeliverySlot> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StoreId == storeId);
        }

        #endregion

    }
}
