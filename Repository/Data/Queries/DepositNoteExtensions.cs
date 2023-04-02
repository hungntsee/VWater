using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class DepositNoteExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.DepositNote GetByKey(this IQueryable<VWater.Data.Entities.DepositNote> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.DepositNote> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.DepositNote> GetByKeyAsync(this IQueryable<VWater.Data.Entities.DepositNote> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.DepositNote> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.DepositNote>(task);
        }

        public static IQueryable<VWater.Data.Entities.DepositNote> ByOrderId(this IQueryable<VWater.Data.Entities.DepositNote> queryable, int orderId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.OrderId == orderId);
        }

        #endregion

    }
}
