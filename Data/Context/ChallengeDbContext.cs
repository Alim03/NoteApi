using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Data.Context
{
    public class ChallengeDbContext : DbContext
    {
        public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Note>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.UtcNow;
                        entry.Entity.DateModified = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateModified = DateTime.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
