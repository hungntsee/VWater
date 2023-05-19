using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VWater.Data.Queries
{
    public static partial class CustomerExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Customer GetByKey(this IQueryable<VWater.Data.Entities.Customer> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Customer> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Customer> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Customer> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Customer> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Customer>(task);
        }

        public static IQueryable<VWater.Data.Entities.Customer> ByCustomerName(this IQueryable<VWater.Data.Entities.Customer> queryable, string? search)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            //return queryable.Where(q => search.All(k => q.CustomerName.Contains(k)));
            return queryable.Where(x => x.FullName.Trim().ToLower().Contains(search.Trim().ToLower()));
        }
        #endregion

    }
}
