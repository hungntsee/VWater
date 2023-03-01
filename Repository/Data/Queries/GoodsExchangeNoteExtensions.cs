using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class GoodsExchangeNoteExtensions
    {
        #region Generated Extensions
        public static IQueryable<VWater.Data.Entities.GoodsExchangeNote> ByGoodsId(this IQueryable<VWater.Data.Entities.GoodsExchangeNote> queryable, int goodsId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.GoodsId == goodsId);
        }

        public static VWater.Data.Entities.GoodsExchangeNote GetByKey(this IQueryable<VWater.Data.Entities.GoodsExchangeNote> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsExchangeNote> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.GoodsExchangeNote> GetByKeyAsync(this IQueryable<VWater.Data.Entities.GoodsExchangeNote> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.GoodsExchangeNote> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.GoodsExchangeNote>(task);
        }

        public static IQueryable<VWater.Data.Entities.GoodsExchangeNote> ByOrderId(this IQueryable<VWater.Data.Entities.GoodsExchangeNote> queryable, int? orderId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => (q.OrderId == orderId || (orderId == null && q.OrderId == null)));
        }

        public static IQueryable<VWater.Data.Entities.GoodsExchangeNote> ByPurchaseOrderId(this IQueryable<VWater.Data.Entities.GoodsExchangeNote> queryable, int purchaseOrderId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.PurchaseOrderId == purchaseOrderId);
        }

        public static IQueryable<VWater.Data.Entities.GoodsExchangeNote> ByWarehouseId(this IQueryable<VWater.Data.Entities.GoodsExchangeNote> queryable, int warehouseId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.WarehouseId == warehouseId);
        }

        #endregion

    }
}
