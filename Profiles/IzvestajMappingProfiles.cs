using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class IzvestajMappingProfiles : Profile
    {
        public IzvestajMappingProfiles()
        {
            CreateMap<Izvestaj, IzvestajResponseDTO>();
            CreateMap<AddIzvestajDTO, Izvestaj>();
            CreateMap<UpdateIzvestajDTO, Izvestaj>();
        }
    }
}
