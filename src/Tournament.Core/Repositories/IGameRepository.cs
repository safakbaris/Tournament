using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface IGameRepository
    {
        Task<Game> Get(int id);

        Task<bool> Update(Game game);

        Task<GameInfoDto> GetGameInfo(int gameId);
        Task<bool> UpdateGameNumber(int gameId, int playerId, int number);
    }
}
