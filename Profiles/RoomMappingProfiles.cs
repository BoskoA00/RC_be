using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class RoomMappingProfiles : Profile
    {
        public RoomMappingProfiles()
        {
            CreateMap<Room, RoomResponseDTO>();
            CreateMap<AddRoomDTO, Room>();
            CreateMap<UpdateRoomDTO, Room>();
        }
    }
}
