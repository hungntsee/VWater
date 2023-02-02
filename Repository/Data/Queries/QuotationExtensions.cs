using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class QuotationExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.Quotation> ByDistributorId(this IQueryable<VWater.Data.Entities.Quotation> queryable, int distributorId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.DistributorId == distributorId);
        }

        public static VWater.Data.Entities.Quotation GetByKey(this IQueryable<VWater.Data.Entities.Quotation> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Quotation> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Quotation> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Quotation> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Quotation> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Quotation>(task);
        }

        #endregion

    }
}
