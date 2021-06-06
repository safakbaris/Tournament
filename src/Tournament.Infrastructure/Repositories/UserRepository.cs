using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repository;

namespace Tournament.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TournamentContext context;

        public UserRepository(TournamentContext context)
        {
            this.context = context;
        }
        public async Task<User> CheckCredentials(string email, string password)
        {
            return await context.Users.Where(p => p.Email == email && p.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User> Get(int id)
        {
            return await context.Users.Where(p => p.Id== id).FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(User user)
        {
            context.Users.Add(user);
            return await context.SaveChangesAsync() > 0;
        }



    }
}
