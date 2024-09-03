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
    public class IzvestajController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IIzvestaj izvestajService;

        public IzvestajController(IMapper _mapper, IIzvestaj izvestaj)
        {
            this.mapper = _mapper;
            this.izvestajService = izvestaj;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<IzvestajResponseDTO>>(await izvestajService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(mapper.Map<IzvestajResponseDTO>(await izvestajService.GetById(id)));
        }
        [HttpGet("getByCode/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            return Ok(mapper.Map<IzvestajResponseDTO>(await izvestajService.GetIzvestajByCode(code)));
        }
        [HttpGet("byDoktor/{idDoktora}")]
        public async Task<IActionResult> GetByDoktor([FromRoute] int idDoktora)
        {
            return Ok(mapper.Map<List<IzvestajResponseDTO>>(await izvestajService.GetIzvestajiByDoktor(idDoktora)));
        }
        [HttpGet("byPacijent/{idPacijenta}")]
        public async Task<IActionResult> GetByPacijent([FromRoute] int idPacijenta)
        {
            return Ok(mapper.Map<List<IzvestajResponseDTO>>(await izvestajService.GetIzvestajiByPacijent(idPacijenta)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateIzvestaj([FromBody] AddIzvestajDTO request)
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
                return BadRequest("Los zahtev");
            }
            if (izvestajService.CodeTaken(request.sifra))
            {
                return BadRequest("Sifra je zauzeta");
            }
            Izvestaj izv = mapper.Map<Izvestaj>(request);
            await izvestajService.CreateIzvestaj(izv);
            return Ok(izv);
            }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIzvestaj([FromBody] UpdateIzvestajDTO request, [FromRoute] int id)
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
                return BadRequest("Los zahtev");
            }
            Izvestaj? izv = await izvestajService.GetById(id);
            if(izv == null)
            {
                return BadRequest("Ne postoji zeljeni izvestaj");
            }

            var idToken = User.FindFirst("id")?.Value;

            if( userRoleValue == (int)UserRole.Doctor)
            {
            if (int.Parse(idToken) != izv.idDoktora)
            {

                return Forbid();
            }
            }

            Izvestaj izvUpdated = mapper.Map<UpdateIzvestajDTO, Izvestaj>(request, izv);
            await izvestajService.UpdateIzvestaj(izvUpdated);
            return Ok(izvUpdated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) == (int)UserRole.Patient) {
                return Forbid();
            }

            Izvestaj izv = await izvestajService.GetById(id);
            if (izv == null) {

                return BadRequest();
            }
            if (int.Parse(userRole) == (int)UserRole.Doctor) {

                var idUser = User.FindFirst("id")?.Value;
                
                if(int.Parse(idUser) != izv.idDoktora)
                {
                    return Forbid();
                }
            }
            await izvestajService.DeleteById(id);
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

            Izvestaj izv = await izvestajService.GetIzvestajByCode(code);
            if (izv == null)
            {

                return BadRequest();
            }
            if (int.Parse(userRole) == (int)UserRole.Doctor)
            {

                var idUser = User.FindFirst("id")?.Value;

                if (int.Parse(idUser) != izv.idDoktora)
                {
                    return Forbid();
                }
            }
            await izvestajService.DeleteByCode(code);
            return NoContent();
        }
        [HttpDelete("byDoktorId/{idDoktora}")]
        public async Task<IActionResult> DeleteByDoktorId([FromRoute] int idDoktora)
        {

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if( int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }

            await izvestajService.DeleteByDoktor(idDoktora);
            return NoContent();
        }
        [HttpDelete("byPacijentId/{idPacijenta}")]
        public async Task<IActionResult> DeleteByPacijentId([FromRoute] int idPacijenta)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }

            await izvestajService.DeleteByPacijent(idPacijenta);
            return NoContent();
        }
    }
}
