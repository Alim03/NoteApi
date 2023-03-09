using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetEagerLoadAsync(int id);
    }
}
