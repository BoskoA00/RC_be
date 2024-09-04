using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IReport reportService;

        public ReportController(IMapper _mapper, IReport rs)
        {
            this.mapper = _mapper;
            this.reportService = rs;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<ReportResponseDTO>>(await reportService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(mapper.Map<ReportResponseDTO>(await reportService.GetById(id)));
        }
        [HttpGet("getByCode/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            return Ok(mapper.Map<ReportResponseDTO>(await reportService.GetReportByCode(code)));
        }
        [HttpGet("byDoctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor([FromRoute] int doctorId)
        {
            return Ok(mapper.Map<List<ReportResponseDTO>>(await reportService.GetReportsByDoctor(doctorId)));
        }
        [HttpGet("byPatient/{patientId}")]
        public async Task<IActionResult> GetByPacijent([FromRoute] int patientId)
        {
            return Ok(mapper.Map<List<ReportResponseDTO>>(await reportService.GetReportsByPatient(patientId)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] AddReportDTO request)
        {

            var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRoleClaim == null)
            {
                return Forbid();
            }
            if (!int.TryParse(userRoleClaim, out var userRoleValue))
            {
                return Forbid();
            }

            if (userRoleValue == (int)UserRole.Patient )
            {
                return Forbid();
            }
            if (request == null)
            {
                return BadRequest("Bad Request.");
            }
            if (reportService.CodeTaken(request.code))
            {
                return BadRequest("Code is taken!");
            }
            Report report = mapper.Map<Report>(request);
            await reportService.CreateReport(report);
            return Ok(report);
            }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportDTO request, [FromRoute] int id)
        {
            var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRoleClaim == null)
            {
                return Forbid();
            }
            if (!int.TryParse(userRoleClaim, out var userRoleValue))
            {
                return Forbid();
            }

            if (userRoleValue == (int)UserRole.Patient)
            {
                return Forbid();
            }

            if (request == null)
            {
                return BadRequest("Bad request.");
            }
            Report? report = await reportService.GetById(id);
            if(report == null)
            {
                return BadRequest("Wanted report doesn't exist.");
            }

            var idToken = User.FindFirst("id")?.Value;

            if( userRoleValue == (int)UserRole.Doctor)
            {
            if (int.Parse(idToken) != report.doctorId)
            {

                return Forbid();
            }
            }

            Report reportUpdated = mapper.Map<UpdateReportDTO, Report>(request, report);
            await reportService.UpdateReport(reportUpdated);
            return Ok(reportUpdated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient) {
                return Forbid();
            }

            Report report = await reportService.GetById(id);
            if (report == null) {

                return BadRequest("Wanted report doesn't exist.");
            }
            if (int.Parse(userRole) == (int)UserRole.Doctor) {

                var idUser = User.FindFirst("id")?.Value;
                
                if(int.Parse(idUser) != report.doctorId)
                {
                    return Forbid();
                }
            }
            await reportService.DeleteById(id);
            return NoContent();
        }
        [HttpDelete("deleteByCode/{code}")]
        public async Task<IActionResult> DeleteByCode([FromRoute] string code)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }

            Report report = await reportService.GetReportByCode(code);
            if (report == null)
            {

                return BadRequest();
            }
            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {

                var idUser = User.FindFirst("id")?.Value;

                if (int.Parse(idUser) != report.doctorId)
                {
                    return Forbid();
                }
            }
            await reportService.DeleteByCode(code);
            return NoContent();
        }
        [HttpDelete("ByDoctorId/{doctorId}")]
        public async Task<IActionResult> DeleteByDoktorId([FromRoute] int doctorId)
        {

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if( int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }

            await reportService.DeleteByDoctor(doctorId);
            return NoContent();
        }
        [HttpDelete("ByPatientId/{patientId}")]
        public async Task<IActionResult> DeleteByPacijentId([FromRoute] int patientId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }

            await reportService.DeleteByPatient(patientId);
            return NoContent();
        }
    }
}
