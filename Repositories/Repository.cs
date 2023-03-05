using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ChallengeDbContext context;
        protected DbSet<T> entities;

        public Repository(ChallengeDbContext context)
        {
            this.context = context;
            entities = this.context.Set<T>();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await entities.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
