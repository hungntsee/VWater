using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class TransactionExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Transaction GetByKey(this IQueryable<VWater.Data.Entities.Transaction> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Transaction> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Transaction> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Transaction> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Transaction> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Transaction>(task);
        }

        public static IQueryable<VWater.Data.Entities.Transaction> ByOrderId(this IQueryable<VWater.Data.Entities.Transaction> queryable, int? orderId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.OrderId == orderId || (orderId == null && q.OrderId == null)));
        }

        public static IQueryable<VWater.Data.Entities.Transaction> ByWalletId(this IQueryable<VWater.Data.Entities.Transaction> queryable, int walletId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.WalletId == walletId);
        }

        #endregion

    }
}
