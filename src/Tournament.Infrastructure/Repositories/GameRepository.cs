using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentContext context;

        public GameRepository(TournamentContext context)
        {
            this.context = context;
        }
        public async Task<Game> Get(int id)
        {
            return await context.Games.Include(p=>p.Tournament).Include(p=>p.Player1).Include("Player1.Tournaments").Include(p => p.Player2).Include("Player2.Tournaments").Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<GameInfoDto> GetGameInfo(int gameId)
        {
            return await context.Games.Where(p => p.Id == gameId).Select(p => new GameInfoDto{LastPlayedPlayerId=  p.LastPlayedUserId, Number = p.Number, MinimumNumber = p.MinimumNumber}).FirstOrDefaultAsync();
        }

       

        public async Task<bool> Update(Game game)
        {
            context.Update(game);
            return await context.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateGameNumber(int gameId,int playerId, int number)
        {
            var game = await context.Games.FindAsync(gameId);
            game.Number = number;
            game.LastPlayedUserId = playerId;
            return await context.SaveChangesAsync() > 0;
        }
    }
}
