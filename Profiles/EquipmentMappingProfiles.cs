using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class EquipmentMappingProfiles : Profile
    {
        public EquipmentMappingProfiles()
        {
            CreateMap<Equipment, EquipmentResponseDTO>();
            CreateMap<AddEquipmentDTO,Equipment>();
            CreateMap<UpdateOpremaDTO, Equipment>();
            CreateMap<Equipment, EquipmentReducedDTO>();
            CreateMap<EquipmentResponseDTO, Equipment>();
        }
    }
}
