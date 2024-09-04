using AutoMapper;
using IS_server.Data;
using IS_server.DTO;

namespace IS_server.Profiles
{
    public class ReportMappingProfiles : Profile
    {
        public ReportMappingProfiles()
        {
            CreateMap<Report, ReportResponseDTO>();
            CreateMap<AddReportDTO, Report>();
            CreateMap<UpdateReportDTO, Report>();
        }
    }
}
