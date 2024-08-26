using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class TerapijeMappingProfiles : Profile
    {
        public TerapijeMappingProfiles()
        {
            CreateMap<Terapija, TerapijeResponseDTO>();
            CreateMap<TerapijeAddDTO, Terapija>();
            CreateMap<TerapijeUpdateDTO, Terapija>();
        }
    }
}
