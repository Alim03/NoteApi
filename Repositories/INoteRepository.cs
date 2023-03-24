using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Repositories
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<IEnumerable<Note>?> GetUserNotes(int userId);
    }
}