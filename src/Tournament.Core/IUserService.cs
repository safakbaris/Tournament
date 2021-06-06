using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;

namespace Tournament.Core
{
    public interface IUserService
    {
        Task<bool> Add(User user);

        Task<User> Login(string userName, string password);

        Task<ServiceResultDto> IsUserAuthenticatedForGame(int userId,int gameId);

    }
}
