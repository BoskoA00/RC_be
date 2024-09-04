using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class SessionMappingProfiles : Profile
    {
        public SessionMappingProfiles()
        {
            CreateMap<Session, SessionResponseDTO>();
            CreateMap<AddSessionDTO, Session>();
            CreateMap<UpdateSessionDTO, Session>();
        }
    }
}
