using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Data.Context;
using Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(ChallengeDbContext context) : base(context) { }
        public async Task<IEnumerable<Note>?> GetUserNotes(int userId)
        {
            var user = await context.Users.Include(u => u.Notes).FirstOrDefaultAsync(user => user.Id == userId);
            return user?.Notes.ToList();
        }
        public bool IsEmailExist(string email)
        {
            return context.Users.Any(user => user.Email == email);
        }
    }
}