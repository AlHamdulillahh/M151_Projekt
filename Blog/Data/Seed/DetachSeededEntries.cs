using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Seed
{
    public static class DetachSeededEntries
    {
        public static void DetachUnchangedEntries(this DataContext context)
        {
            var unchangedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in unchangedEntries)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
