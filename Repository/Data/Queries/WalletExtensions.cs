using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class WalletExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Wallet GetByKey(this IQueryable<VWater.Data.Entities.Wallet> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Wallet> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Wallet> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Wallet> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Wallet> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Wallet>(task);
        }

        public static IQueryable<VWater.Data.Entities.Wallet> ByShipperId(this IQueryable<VWater.Data.Entities.Wallet> queryable, int shipperId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.ShipperId == shipperId);
        }

        #endregion

    }
}
