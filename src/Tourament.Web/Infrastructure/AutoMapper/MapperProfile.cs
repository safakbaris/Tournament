using AutoMapper;
using Tourament.Web.Models;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;

namespace Tourament.Web.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<JoinTournamentModel, JoinTournamentDto>();
            CreateMap<CreateTournamentModel, Tournament.Core.Entities.Tournament>();
            CreateMap<RegisterModel, User>();
        }
    }
}
