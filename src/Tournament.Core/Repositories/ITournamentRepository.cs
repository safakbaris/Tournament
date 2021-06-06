using System.Collections.Generic;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<bool> Insert(Entities.Tournament tournament);

        Task<IEnumerable<TournamentDto>> GetAll();

        Task<Entities.Tournament> Get(int id);

        Task<bool> AddPlayer(int tournamentId, int userId);

        Task<bool> IsUserAttendedToTournament(int tournamentId, int userId);

        Task<bool> Delete(int id);

        Task<bool> Update(Entities.Tournament tournament);

        Task<int> GetPlayingOrWaitingTournamentPlayersCount(int tournamentId);
    }
}
