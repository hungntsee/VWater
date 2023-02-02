using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class GoodsInQuotationExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.GoodsInQuotation> ByGoodsId(this IQueryable<VWater.Data.Entities.GoodsInQuotation> queryable, int goodsId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.GoodsId == goodsId);
        }

        public static VWater.Data.Entities.GoodsInQuotation GetByKey(this IQueryable<VWater.Data.Entities.GoodsInQuotation> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsInQuotation> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.GoodsInQuotation> GetByKeyAsync(this IQueryable<VWater.Data.Entities.GoodsInQuotation> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsInQuotation> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.GoodsInQuotation>(task);
        }

        public static IQueryable<VWater.Data.Entities.GoodsInQuotation> ByQuotationId(this IQueryable<VWater.Data.Entities.GoodsInQuotation> queryable, int quotationId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.QuotationId == quotationId);
        }

        #endregion

    }
}
