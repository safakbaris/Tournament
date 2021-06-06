using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;

namespace Tournament.Core
{
    public interface IGameService
    {

        Task<GameInfoDto> GetGameInfo(int gameId);
        Task<ServiceResultDto<int>> Play(int gameId,int playerId, int number);
       

    }
}
