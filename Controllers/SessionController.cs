using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using IS_server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly Interfaces.ISession sessionService;
        private readonly ITherapy therapyService;
        private readonly IMapper mapper;

        public SessionController(ITherapy therapyService, Interfaces.ISession sessionService, IMapper _mapper)
        {
            this.sessionService = sessionService;
            this.mapper = _mapper;
            this.therapyService = therapyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<SessionResponseDTO>>(await sessionService.GetAllSessions()));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSessionById([FromRoute] int id)
        {
            return Ok(mapper.Map<SessionResponseDTO>(await sessionService.GetSessionById(id)));
        }
        [HttpGet("SessionsByTherapy/{id}")]
        public async Task<IActionResult> GetSessionsByTherapyId([FromRoute] int id)
        {
            return Ok(mapper.Map<List<SessionResponseDTO>>(await sessionService.GetSessionsByTherapyId(id)));
        }
        [HttpGet("SessionsByTherapyCode/{code}")]
        public async Task<IActionResult> GetSessionsByTherapyCode([FromRoute] string code)
        {
            return Ok(mapper.Map<List<SessionResponseDTO>>(await sessionService.GetSessionsByTherapyCode(code)));
        }
        [HttpGet("SessionsByDoctorId/{id}")]
        public async Task<IActionResult> GetSessionsByDoctorId([FromRoute]int id)
        {
            return Ok(mapper.Map<List<SessionResponseDTO>>(await sessionService.GetSessionsByDoctorId(id)));
        }
        [HttpGet("SessionsByPatientId/{id}")]
        public async Task<IActionResult> GetSessionsByPatientId([FromRoute] int id)
        {
            return Ok(mapper.Map<List<SessionResponseDTO>>(await sessionService.GetSessionsByPatientId(id)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateSessions([FromBody] AddSessionDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }
            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }

            if(request == null)
            {
                return BadRequest("Bad request.");
            }
            Session session = await sessionService.CreateSession(mapper.Map<Session>(request));
            return Ok(session);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession([FromRoute] int id,[FromBody] UpdateSessionDTO request)
        {

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }
            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }

            Session session = await sessionService.GetSessionById(id);
            if(session == null)
            {
                return BadRequest("Wanted sessions doesn't exist.");
            }
            if(request == null)
            {
                return BadRequest("Bad request.");
            }
            Session sessionUpdated = mapper.Map<UpdateSessionDTO, Session>(request, session);
            await sessionService.UpdateSession(sessionUpdated);
            return Ok(sessionUpdated);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSession([FromRoute] int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }
            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }

            Session session = await sessionService.GetSessionById(id);
            Therapy therapy = await therapyService.GetTherapyById(session.therapyId);

            var idToken = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {
                if (therapy.doctorId != int.Parse(idToken)) {
                    return Forbid();
                }
            }



            await sessionService.DeleteSession(id);
            return NoContent();
        }
        [HttpDelete("Therapies/{therapyId}")]
        public async Task<IActionResult> DeleteSessionsByTherapy([FromRoute] int therapyId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }
            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }

            Therapy therapy = await therapyService.GetTherapyById(therapyId);

            var idToken = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {
                if (therapy.doctorId != int.Parse(idToken))
                {
                    return Forbid();
                }
            }


            await sessionService.DeleteSessionsByTherapy(therapyId);
            return NoContent();

        }




    }
}
