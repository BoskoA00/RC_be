using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class TherapyMappingProfiles : Profile
    {
        public TherapyMappingProfiles()
        {
            CreateMap<Therapy, TherapyResponseDTO>();
            CreateMap<TherapyAddDTO, Therapy>();
            CreateMap<TherapyUpdateDTO, Therapy>();
        }
    }
}
