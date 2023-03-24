using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetEagerLoadAsync(int id);
        bool IsEmailExist(string email);
        User? GetByEmail(string email);
    }
}
