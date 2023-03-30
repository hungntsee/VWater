using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VWater.Data.Queries
{
    public static partial class SysdiagramsExtensions
    {
        #region Generated Extensions
        public static VWater.Data.Entities.Sysdiagrams GetByKey(this IQueryable<VWater.Data.Entities.Sysdiagrams> queryable, int diagramId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Sysdiagrams> dbSet)
                return dbSet.Find(diagramId);

            return queryable.FirstOrDefault(q => q.DiagramId == diagramId);
        }

        public static ValueTask<VWater.Data.Entities.Sysdiagrams> GetByKeyAsync(this IQueryable<VWater.Data.Entities.Sysdiagrams> queryable, int diagramId)
        {
            if (queryable is null)
                throw new ArgumentNullException(nameof(queryable));

            if (queryable is DbSet<VWater.Data.Entities.Sysdiagrams> dbSet)
                return dbSet.FindAsync(diagramId);

            var task = queryable.FirstOrDefaultAsync(q => q.DiagramId == diagramId);
            return new ValueTask<VWater.Data.Entities.Sysdiagrams>(task);
        }

        #endregion

    }
}
