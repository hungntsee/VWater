using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class DeliveryAddressExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.DeliveryAddress> ByBuildingId(this IQueryable<VWater.Data.Entities.DeliveryAddress> queryable, int? buildingId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.BuildingId == buildingId || (buildingId == null && q.BuildingId == null)));
        }

        public static IQueryable<VWater.Data.Entities.DeliveryAddress> ByCustomerId(this IQueryable<VWater.Data.Entities.DeliveryAddress> queryable, int customerId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.CustomerId == customerId);
        }

        public static IQueryable<VWater.Data.Entities.DeliveryAddress> ByDeliveryTypeId(this IQueryable<VWater.Data.Entities.DeliveryAddress> queryable, int deliveryTypeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.DeliveryTypeId == deliveryTypeId);
        }

        public static VWater.Data.Entities.DeliveryAddress GetByKey(this IQueryable<VWater.Data.Entities.DeliveryAddress> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.DeliveryAddress> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.DeliveryAddress> GetByKeyAsync(this IQueryable<VWater.Data.Entities.DeliveryAddress> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.DeliveryAddress> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.DeliveryAddress>(task);
        }

        public static IQueryable<VWater.Data.Entities.DeliveryAddress> ByStoreId(this IQueryable<VWater.Data.Entities.DeliveryAddress> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.StoreId == storeId);
        }

        #endregion

    }
}
