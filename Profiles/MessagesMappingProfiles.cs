using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class MessagesMappingProfiles : Profile
    {
        public MessagesMappingProfiles()
        {
            CreateMap<MessageRequestDTO, Message>();
            CreateMap< Message,MessageResponseDTO>();
        }
    }
}
