using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class AccountExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Account GetByKey(this IQueryable<VWater.Data.Entities.Account> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Account> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<VWater.Data.Entities.Account> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Account> queryable, int id)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Account> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<VWater.Data.Entities.Account>(task);
        }

        public static IQueryable<VWater.Data.Entities.Account> ByRoleId(this IQueryable<VWater.Data.Entities.Account> queryable, int roleId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            return queryable.Where(q => q.RoleId == roleId);
        }

        #endregion

    }
}
