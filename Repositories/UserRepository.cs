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
        public User? GetEagerLoadAsync(int id)
        {
            var user =  context.Users.Where(x => x.Id == id).Include(x => x.Notes).ToList().FirstOrDefault();
            return user;
        }
    }
}
