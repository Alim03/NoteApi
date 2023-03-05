using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Repositories
{
    public interface IRepository<T>
    {
        Task<T?> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T item);

        Task SaveAsync();
    }
}
