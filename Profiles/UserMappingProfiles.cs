using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class UserMappingProfiles : Profile
    {
        public UserMappingProfiles()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRegisterDTO, User>();
        }
    }
}
