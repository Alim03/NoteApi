using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Data.Context;
using Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChallengeDbContext context) : base(context) { }

        public async Task<User?> GetEagerLoadAsync(int id)
        {
            return await context.Users
                .Include(x => x.Notes)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public bool IsEmailExist(string email)
        {
            return context.Users.Any(user => user.Email == email);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
