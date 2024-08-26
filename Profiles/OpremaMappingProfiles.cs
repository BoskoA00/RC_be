using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class OpremaMappingProfiles : Profile
    {
        public OpremaMappingProfiles()
        {
            CreateMap<Oprema, OpremaResponseDTO>();
            CreateMap<AddOpremaDTO,Oprema>();
            CreateMap<UpdateOpremaDTO, Oprema>();
            CreateMap<Oprema, OpremaReducedDTO>();
            CreateMap<OpremaResponseDTO, Oprema>();
        }
    }
}
