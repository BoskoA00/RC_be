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
    public class SesijaController : ControllerBase
    {
        private readonly ISesija ss;
        private readonly ITerapija ts;
        private readonly IMapper mapper;

        public SesijaController(ITerapija terapijaService, ISesija sesijaService, IMapper _mapper)
        {
            this.ss = sesijaService;
            this.mapper = _mapper;
            this.ts = terapijaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<SesijaResponseDTO>>(await ss.GetAllSesije()));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSesijaById([FromRoute] int id)
        {
            return Ok(mapper.Map<SesijaResponseDTO>(await ss.GetSesijaById(id)));
        }
        [HttpGet("sesijeByTerapija/{id}")]
        public async Task<IActionResult> GetSesijeByTerapija([FromRoute] int id)
        {
            return Ok(mapper.Map<List<SesijaResponseDTO>>(await ss.GetSesijeByTerapija(id)));
        }
        [HttpGet("sesijeByTerapijaCode/{code}")]
        public async Task<IActionResult> GetSesijeByTerapijaCode([FromRoute] string code)
        {
            return Ok(mapper.Map<List<SesijaResponseDTO>>(await ss.GetSesijaByTerapijaCode(code)));
        }
        [HttpGet("sesijeByDoktorId/{id}")]
        public async Task<IActionResult> GetSesijeByDoktorId([FromRoute]int id)
        {
            return Ok(mapper.Map<List<SesijaResponseDTO>>(await ss.GetSesijaByIdDoktora(id)));
        }
        [HttpGet("sesijeByPacijentId/{id}")]
        public async Task<IActionResult> GetSesijeByPacijentId([FromRoute] int id)
        {
            return Ok(mapper.Map<List<SesijaResponseDTO>>(await ss.GetSesijeByIdPacijent(id)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateSesija([FromBody] AddSesijaDTO request)
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
                return BadRequest("Los zahtev");
            }
            Sesija s = await ss.CreateSesija(mapper.Map<Sesija>(request));
            return Ok(s);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSesija([FromRoute] int id,[FromBody] UpdateSesijaDTO request)
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

            Sesija s = await ss.GetSesijaById(id);
            if(s == null)
            {
                return BadRequest("Ne postoji trazena sesija");
            }
            if(request == null)
            {
                return BadRequest("Los zahtev");
            }
            Sesija sesijaUpdated = mapper.Map<UpdateSesijaDTO, Sesija>(request, s);
            await ss.UpdateSesija(sesijaUpdated);
            return Ok(sesijaUpdated);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSesija([FromRoute] int id)
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

            Sesija s = await ss.GetSesijaById(id);
            Terapija t = await ts.GetTerapijaById(s.idTerapije);

            var idToken = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {
                if (t.idDoktora != int.Parse(idToken)) {
                    return Forbid();
                }
            }



            await ss.DeleteSesija(id);
            return NoContent();
        }
        [HttpDelete("Terapije/{idTerapije}")]
        public async Task<IActionResult> DeleteSesijeByTerapija([FromRoute] int idTerapije)
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

            Terapija t = await ts.GetTerapijaById(idTerapije);

            var idToken = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {
                if (t.idDoktora != int.Parse(idToken))
                {
                    return Forbid();
                }
            }


            await ss.DeleteSesijeByTerapija(idTerapije);
            return NoContent();

        }




    }
}
