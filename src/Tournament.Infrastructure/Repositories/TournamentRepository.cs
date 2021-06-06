using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using static Tournament.Core.Enum.Enums;

namespace Tournament.Infrastructure.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentContext context;

        public TournamentRepository(TournamentContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddPlayer(int tournamentId, int userId)
        {

            context.TournamentPlayers.Add(new TournamentPlayer { UserId = userId, TournamentId = tournamentId });
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await context.Tournaments.FindAsync(id);
            if (entity != null)
            {
                context.Tournaments.Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Core.Entities.Tournament> Get(int id)
        {
            return await context.Tournaments.Include(p => p.Players).Include("Players.User").DefaultIfEmpty().Where(p => p.Id == id).FirstOrDefaultAsync();
         
        }

        public async Task<IEnumerable<TournamentDto>> GetAll()
        {
            return await context.Tournaments.Include(p => p.Players).Select(p => new TournamentDto
            {
                Id = p.Id,
                TournamentName = p.Name,
                MinimumPlayers = p.MinimumPlayers,
                MaximumPlayers = p.MaximumPlayers,
                StartTime = p.StartTime,
                Status = p.Status.ToString(),
                WaitingPlayers = p.Players.Where(p=>p.Status == TournamentPlayerStatus.Waiting).Count(),
                PlayingPlayers = p.Players.Where(p => p.Status == TournamentPlayerStatus.Playing).Count()
            }).ToListAsync();
        }

        public async Task<int> GetPlayingOrWaitingTournamentPlayersCount(int tournamentId)
        {
          return  await context.TournamentPlayers.Where(p => p.TournamentId == tournamentId && (p.Status == TournamentPlayerStatus.Waiting || p.Status == TournamentPlayerStatus.Playing)).CountAsync();
        }

        public async Task<bool> Insert(Core.Entities.Tournament tournament)
        {
            context.Add(tournament);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsUserAttendedToTournament(int tournamentId, int userId)
        {
            return await context.TournamentPlayers.Where(p => p.TournamentId == tournamentId && p.UserId == userId).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> Update(Core.Entities.Tournament tournament)
        {
            context.Update(tournament);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
