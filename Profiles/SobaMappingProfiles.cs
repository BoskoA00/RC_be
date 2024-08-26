using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class SobaMappingProfiles : Profile
    {
        public SobaMappingProfiles()
        {
            CreateMap<Soba, SobaResponseDTO>();
            CreateMap<AddSobaDTO, Soba>();
            CreateMap<UpdateSobaDTO, Soba>();
        }
    }
}
