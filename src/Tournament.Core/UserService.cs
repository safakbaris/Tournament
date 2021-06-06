using System;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Core.Repository;

namespace Tournament.Core
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IGameRepository gameRepository;

        public UserService(IUserRepository repository, IGameRepository gameRepository)
        {
            this.repository = repository;
            this.gameRepository = gameRepository;
        }
        public async Task<bool> Add(User user)
        {

            return await repository.Insert(user);
        }

        public async Task<ServiceResultDto> IsUserAuthenticatedForGame(int userId, int gameId)
        {
            ServiceResultDto result = new ServiceResultDto();
            var game = await gameRepository.Get(gameId);
            if (game != null)
            {
                if (game.Player1Id == userId || game.Player2Id == userId)
                    result.Success = true;
                else
                {
                    result.Message = "You are not authorized to enter this game.";
                }
            }
            else
            {
                result.Message = "Game not found.";
            }
            return result;
        }

        public async Task<User> Login(string userName, string password)
        {
            return await repository.CheckCredentials(userName, password);
        }


    }
}
