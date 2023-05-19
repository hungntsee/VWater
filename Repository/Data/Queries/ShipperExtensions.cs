using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class ShipperExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Shipper> ByAccountId(this IQueryable<VWater.Data.Entities.Shipper> queryable, int accountId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.AccountId == accountId);
        }

        public static VWater.Data.Entities.Shipper GetByKey(this IQueryable<VWater.Data.Entities.Shipper> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Shipper> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Shipper> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Shipper> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Shipper> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Shipper>(task);
        }

        public static IQueryable<VWater.Data.Entities.Shipper> ByStoreId(this IQueryable<VWater.Data.Entities.Shipper> queryable, int storeId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));
            queryable.Include(x => x.Account);
            return queryable.Where(q => q.Account.StoreId == storeId);
        }

        public static IQueryable<VWater.Data.Entities.Shipper> ByShipperName(this IQueryable<VWater.Data.Entities.Shipper> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.ShipperName.Contains(k)));
            return queryable.Where(x => x.Fullname.Trim().ToLower().Contains(search.Trim().ToLower()));
        }
        #endregion

    }
}
