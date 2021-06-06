using Hangfire;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using static Tournament.Core.Enum.Enums;
using System.Reflection;

namespace Tournament.Core
{
    public class GameService : IGameService
    {
        private readonly IGameRepository repository;
        private readonly ITournamentRepository tournamentRepository;
        private readonly ITournamentService tournamentService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GameService(IGameRepository repository, ITournamentRepository tournamentRepository, ITournamentService tournamentService, IBackgroundJobClient backgroundJobClient, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.tournamentRepository = tournamentRepository;
            this.tournamentService = tournamentService;
            this.backgroundJobClient = backgroundJobClient;
            this.httpContextAccessor = httpContextAccessor;
        }



        public async Task<GameInfoDto> GetGameInfo(int gameId)
        {
            return await repository.GetGameInfo(gameId);
        }


        public async Task<ServiceResultDto<int>> Play(int gameId, int userId, int number)
        {
            ServiceResultDto<int> result = new ServiceResultDto<int>();
            if (number==2|| number ==3)
            {
                var game = await repository.Get(gameId);
                if (game.LastPlayedUserId == 0)
                    game.LastPlayedUserId = game.Player2Id;
                if (game.WinnerPlayerId == 0)
                {
                    if (game.LastPlayedUserId != userId)
                    {
                        if (game.Number - number <= game.MinimumNumber)
                        {
                            TournamentPlayerStatus winnerStatus;
                            game.WinnerPlayerId = userId;
                            var waitingOrPlayingPlayersCount = await tournamentRepository.GetPlayingOrWaitingTournamentPlayersCount(game.TournamentId);
                            if (waitingOrPlayingPlayersCount > 2)
                            {
                                result.Message = "You won the game.";
                                winnerStatus = TournamentPlayerStatus.Waiting;
                                var httpContext = httpContextAccessor.HttpContext;
                                var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";
                                backgroundJobClient.Enqueue(() => tournamentService.ArrangeGames(game.TournamentId, baseUrl));
                            }
                            else
                            {
                                result.Message = "You won the tournament.";
                                winnerStatus = TournamentPlayerStatus.Winner;
                                game.Tournament.Status = TournamentStatus.Finished;
                            }
                            string winnerPlayerPropertyName = "", eliminatedPlayerPropertyName = "";
                            if (game.Player1Id == userId)
                            {
                                winnerPlayerPropertyName = "Player1";
                                eliminatedPlayerPropertyName = "Player2";
                            }
                            else
                            {
                                winnerPlayerPropertyName = "Player2";
                                eliminatedPlayerPropertyName = "Player1";
                            }

                            var winnerPlayer = (game.GetType().GetProperty(winnerPlayerPropertyName).GetValue(game, null) as User);
                            var eliminatedPlayer = (game.GetType().GetProperty(eliminatedPlayerPropertyName).GetValue(game, null) as User);

                            winnerPlayer.Tournaments.Where(p => p.TournamentId == game.TournamentId).FirstOrDefault().Status = winnerStatus;
                            eliminatedPlayer.Tournaments.Where(p => p.TournamentId == game.TournamentId).FirstOrDefault().Status = TournamentPlayerStatus.Eliminated;
                        }
                        else
                        {
                            game.Number -= number;
                            game.LastPlayedUserId = userId;
                            if (game.Logs == null)
                                game.Logs = new List<GameLog>();

                            game.Logs.Add(new GameLog { Game = game, UserId = userId, PlayedNumber = number });
                        }


                        result.Success = await repository.Update(game);
                        if (!result.Success)
                            result.Message = "Play is not successful.";
                        else
                            result.Data = game.Number;

                    }
                    else
                        result.Message = "It is not your turn.";
                }
                else
                {
                    if (game.WinnerPlayerId == userId)
                    {
                        result.Message = "You already won the game";
                    }
                    else
                    {
                        result.Message = "Your opponent won the game";
                    }
                }
            }
            else
                result.Message = "You can only enter 2 or 3";




            return result;
        }
    }
}
