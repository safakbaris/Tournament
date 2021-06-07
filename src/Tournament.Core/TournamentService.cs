using FluentEmail.Core;
using Hangfire;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Core.Repository;
using static Tournament.Core.Enum.Enums;

namespace Tournament.Core
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository tournamentRepository;
        private readonly IUserRepository userRepository;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IFluentEmail email;

        public TournamentService(ITournamentRepository tournamentRepository, IUserRepository userRepository, IBackgroundJobClient backgroundJobClient,IFluentEmail email)
        {
            this.tournamentRepository = tournamentRepository;
            this.userRepository = userRepository;
            this.backgroundJobClient = backgroundJobClient;
            this.email = email;
        }

        public async Task<bool> Add(Entities.Tournament tournament,string baseUrl)
        {

            bool result = await tournamentRepository.Insert(tournament);
            if (result)
                backgroundJobClient.Schedule(() => ArrangeGames(tournament.Id,baseUrl), tournament.StartTime.ToUniversalTime());
            return result;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task ArrangeGames(int tournamentId,string baseUrl)
        {
            List<int> retainedPlayerIds = new List<int>();
            List<Game> games = new List<Game>();
            var tournament = await tournamentRepository.Get(tournamentId);
            if (tournament!=null)
            {
                retainedPlayerIds.AddRange(tournament.Players.Where(p=>p.Status== TournamentPlayerStatus.Waiting).Select(p => p.Id));
                if (retainedPlayerIds.Count < tournament.MinimumPlayers && tournament.Status == TournamentStatus.NotStarted)
                    tournament.Status = TournamentStatus.Cancelled;
                else
                {
                    tournament.Status = TournamentStatus.Started;
                    Random randomGenerator = new Random();
                    while (retainedPlayerIds.Any() && retainedPlayerIds.Count > 1)
                    {
                        List<int> randomPlayerIds = retainedPlayerIds.OrderBy(x => randomGenerator.Next()).Take(2).ToList();
                        if (tournament.Games == null)
                            tournament.Games = new List<Game>();

                        var tournamentPlayer1 = tournament.Players.Where(p => p.Id == randomPlayerIds[0]).First();
                        tournamentPlayer1.Status = TournamentPlayerStatus.Playing;
                        var tournamentPlayer2 = tournament.Players.Where(p => p.Id == randomPlayerIds[1]).First();
                        tournamentPlayer2.Status = TournamentPlayerStatus.Playing;
                        tournament.Games.Add(
                            new Game
                            {
                                Tournament = tournament,
                                Player1 = tournamentPlayer1.User,
                                Player2 = tournamentPlayer2.User,
                                Number = new Random().Next(20, 60),
                                MinimumNumber=4
                            });
                        foreach (var id in randomPlayerIds)
                            retainedPlayerIds.Remove(id);
                    }
                }
                if (!await tournamentRepository.Update(tournament))
                    throw new Exception("Error on update!");
                else
                {
                    if (tournament.Games!= null)
                    {
                       
                        string gameReadySubject = "Game is Ready!";
                            
                        foreach (var game in tournament.Games)
                        {
                            var gameUrl = $"{baseUrl}/Game/Index/";
                            gameUrl += $"{game.Id}";
                            string body = $"You can join your game from link above. {Environment.NewLine} {gameUrl}";
                            backgroundJobClient.Schedule(() => SendEmail(game.Player1.Email, gameReadySubject, body), tournament.StartTime.ToUniversalTime());
                            backgroundJobClient.Schedule(() => SendEmail(game.Player2.Email, gameReadySubject, body), tournament.StartTime.ToUniversalTime());
                        }
                    }
                      
                   
                }
            }
           
        }

        public async Task SendEmail(string emailAddress,string subject,string body) 
        {
            email.To(emailAddress)
                 .Subject(subject)
                 .Body(body);
            await email.SendAsync();
        }
        public async Task<IEnumerable<TournamentDto>> Get()
        {
            return await tournamentRepository.GetAll();
        }

        public async Task<ServiceResultDto> JoinTournament(JoinTournamentDto dto)
        {

            ServiceResultDto result = new ServiceResultDto();
            var tournament = await tournamentRepository.Get(dto.TournamentId);
            if (tournament.Status == TournamentStatus.NotStarted)
            {
                if (!await tournamentRepository.IsUserAttendedToTournament(dto.TournamentId, dto.PlayerId))
                {
                    var user = await userRepository.Get(dto.PlayerId);
                    if (user != null)
                    {

                        if (tournament != null)
                        {

                            result.Success = await tournamentRepository.AddPlayer(dto.TournamentId, dto.PlayerId);
                            if (result.Success)
                            {
                                backgroundJobClient.Schedule(() => SendEmail(user.Email, "Tournament is starting in 1 hour!", "Check out your email you will get an game url at start time."), tournament.StartTime.AddHours(-1).ToUniversalTime());
                                result.Message = "Successfully attended to tournament.";
                            }
                        }

                    }

                }
                else
                {
                    result.Message = "You are already attended the tournament.";
                }
            }
            else
            {
                result.Message = $"You can join a Not Started tournament. Tournament Status:{tournament.Status.ToString()}";
            }
           
            return result;
        }

        public async Task<bool> Remove(int id)
        {
            return await tournamentRepository.Delete(id);
        }
    }
}
