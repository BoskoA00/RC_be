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
    public class TerapijeController : ControllerBase
    {
        private readonly ITerapija ts;
        private readonly IMapper mapper;

        public TerapijeController(ITerapija terapijeService, IMapper mp)
        {
            this.ts = terapijeService;
            this.mapper = mp;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<TerapijeResponseDTO>>(await ts.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTerapijaById([FromRoute] int id)
        {
            return Ok(mapper.Map<TerapijeResponseDTO>(await ts.GetTerapijaById(id)));
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetTerapijaByCode([FromRoute] string code)
        {
            return Ok(mapper.Map<TerapijeResponseDTO>(await ts.GetTerapijaByCode(code)));
        }

        [HttpGet("doktor/{id}")]
        public async Task<IActionResult> GetTerapijeByDoktorId([FromRoute] int id)
        {
            return Ok(mapper.Map<List<TerapijeResponseDTO>>(await ts.GetTerapijeByDoktor(id)));
        }

        [HttpGet("pacijent/{id}")]
        public async Task<IActionResult> GetTerapijeByPacijent([FromRoute] int id)
        {
            return Ok(mapper.Map<List<TerapijeResponseDTO>>(await ts.GetTerapijeByPacijent(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTerapija([FromBody] TerapijeAddDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient) {
                return Forbid();
            }
            if (request == null)
            {
                return BadRequest("Los zahtev");
            }
            if (ts.CodeTaken(request.sifra))
            {
                return BadRequest("Sifra terapije je zauzeta");
            }
            Terapija terapija = mapper.Map<Terapija>(request);
            await ts.CreateTerapija(terapija);
            return Ok(mapper.Map<TerapijeResponseDTO>(terapija));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTerapija( [FromBody] TerapijeUpdateDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }



            Terapija? terapija = await ts.GetTerapijaById(request.id);

            var idUser = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {
                if (terapija.idDoktora != int.Parse(idUser)) { 
                return Forbid();
                }
            }


            if (terapija == null)
            {
                return NotFound();
            }
            if (request == null)
            {
                return BadRequest("Los zahtev");
            }
            Terapija terapijaUpdated = mapper.Map<TerapijeUpdateDTO, Terapija>(request, terapija);
            await ts.UpdateTerapija(terapijaUpdated);
            return Ok(terapijaUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient)
            {
                return Forbid();
            }
            Terapija terapija = await ts.GetTerapijaById(id);
            
            if (terapija== null)
            {
                return NotFound("Ne postoji trazena terapija");
            }

            var idUser = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor && terapija.idDoktora != int.Parse(idUser))
            {
                    return Forbid();
            }

            await ts.DeleteTerapijaById(id);
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
            Terapija terapija = await ts.GetTerapijaByCode(code);

            if (terapija == null)
            {
                return NotFound("Ne postoji trazena terapija");
            }

            var idUser = User.FindFirst("id")?.Value;

            if (int.Parse(userRole) == (int)UserRole.Doctor && terapija.idDoktora != int.Parse(idUser))
            {
                return Forbid();
            }
            await ts.DeleteTerapijaByCode(code);
            return Ok("Terapija je izbrisana");
        }
    }
}
