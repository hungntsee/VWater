using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class PurchaseOrderDetailExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.PurchaseOrderDetail> ByGoodsId(this IQueryable<VWater.Data.Entities.PurchaseOrderDetail> queryable, int goodsId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.GoodsId == goodsId);
        }

        public static VWater.Data.Entities.PurchaseOrderDetail GetByKey(this IQueryable<VWater.Data.Entities.PurchaseOrderDetail> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.PurchaseOrderDetail> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.PurchaseOrderDetail> GetByKeyAsync(this IQueryable<VWater.Data.Entities.PurchaseOrderDetail> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.PurchaseOrderDetail> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.PurchaseOrderDetail>(task);
        }

        public static IQueryable<VWater.Data.Entities.PurchaseOrderDetail> ByPurchaseOrderId(this IQueryable<VWater.Data.Entities.PurchaseOrderDetail> queryable, int purchaseOrderId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.PurchaseOrderId == purchaseOrderId);
        }

        #endregion

    }
}
