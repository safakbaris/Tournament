using System.Collections.Generic;
using System.Threading.Tasks;
using Tournament.Core.Dtos;

namespace Tournament.Core
{
    public interface ITournamentService
    {
        Task<bool> Add(Entities.Tournament tournament,string baseUrl);
        Task<ServiceResultDto> JoinTournament(JoinTournamentDto dto);

        Task<IEnumerable<TournamentDto>> Get();

        Task ArrangeGames(int tournamentId,string baseUrl);

        Task<bool> Remove(int id);

        Task SendEmail(string emailAddress, string subject, string body);

      
    }
}
