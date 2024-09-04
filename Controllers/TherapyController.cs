using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using IS_server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapyController : ControllerBase
    {
        private readonly ITherapy therapyService;
        private readonly IMapper mapper;

        public TherapyController(ITherapy _therapyService, IMapper _mapper)
        {
            this.therapyService = _therapyService;
            this.mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<TherapyResponseDTO>>(await therapyService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTherapyById([FromRoute] int id)
        {
            return Ok(mapper.Map<TherapyResponseDTO>(await therapyService.GetTherapyById(id)));
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetTherapyByCode([FromRoute] string code)
        {
            return Ok(mapper.Map<TherapyResponseDTO>(await therapyService.GetTherapyByCode(code)));
        }

        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetTherapiesByDoctorId([FromRoute] int id)
        {
            return Ok(mapper.Map<List<TherapyResponseDTO>>(await therapyService.GetTherapiesByDoctorId(id)));
        }

        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetTheraiesByPatientId([FromRoute] int id)
        {
            return Ok(mapper.Map<List<TherapyResponseDTO>>(await therapyService.GetTherapiesByPatientId(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTherapy([FromBody] TherapyAddDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient) {
                return Forbid();
            }
            if (request == null)
            {
                return BadRequest("Bad request.");
            }
            if (therapyService.CodeTaken(request.code))
            {
                return BadRequest("Code taken.");
            }
            Therapy therapy = mapper.Map<Therapy>(request);
            await therapyService.CreateTherapy(therapy);
            return Ok(mapper.Map<TherapyResponseDTO>(therapy));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTherapy( [FromBody] TherapyUpdateDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }



            Therapy? therapy = await therapyService.GetTherapyById(request.id);

            var idUser = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {
                if (therapy.doctorId != int.Parse(idUser)) { 
                return Forbid();
                }
            }


            if (therapy == null)
            {
                return NotFound();
            }
            if (request == null)
            {
                return BadRequest("Bad request.");
            }
            Therapy updatedTherapy = mapper.Map<TherapyUpdateDTO, Therapy>(request, therapy);
            await therapyService.UpdateTherapy(updatedTherapy);
            return Ok(updatedTherapy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }
            Therapy therapy = await therapyService.GetTherapyById(id);
            
            if (therapy== null)
            {
                return NotFound("Wanted therapy doesn't exist.");
            }

            var idUser = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor && therapy.doctorId != int.Parse(idUser))
            {
                    return Forbid();
            }

            await therapyService.DeleteTherapyById(id);
            return NoContent();
        }

        [HttpDelete("code/{code}")]
        public async Task<IActionResult> DeleteByCode([FromRoute] string code)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }
            Therapy therapy = await therapyService.GetTherapyByCode(code);

            if (therapy == null)
            {
                return NotFound("Wanted therapy doesn't exist.");
            }

            var idUser = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor && therapy.doctorId != int.Parse(idUser))
            {
                return Forbid();
            }
            await therapyService.DeleteTherapyByCode(code);
            return NoContent();
        }
    }
}
