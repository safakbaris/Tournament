
using System.Threading.Tasks;
using Tournament.Core.Entities;


namespace Tournament.Core.Repository
{
    public interface IUserRepository
    {
        Task<User> CheckCredentials(string username, string password);

        Task<bool> Insert(User user);

        Task<User> Get(int id);


    }
}
