using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class SesijaMappingProfiles : Profile
    {
        public SesijaMappingProfiles()
        {
            CreateMap<Sesija, SesijaResponseDTO>();
            CreateMap<AddSesijaDTO, Sesija>();
            CreateMap<UpdateSesijaDTO, Sesija>();
        }
    }
}
