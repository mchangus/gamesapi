using AutoMapper;
using Core = Games.Core.Models;

namespace GamesApi.Rawg.Services.Models
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Game, Core.Game>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
