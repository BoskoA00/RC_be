using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpremaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOprema os;

        public OpremaController(IMapper _mapper, IOprema opremaService)
        {
            this.mapper = _mapper;
            this.os = opremaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var opremaList = await os.GetAll();
            return Ok(mapper.Map<List<OpremaResponseDTO>>(opremaList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOpremaById([FromRoute] int id)
        {
            var oprema = await os.GetById(id);
            return Ok(mapper.Map<OpremaResponseDTO>(oprema));
        }

        [HttpGet("bySobaBr/{brSobe}")]
        public async Task<IActionResult> GetOpremaByBrSobe([FromRoute] string brSobe)
        {
            var oprema = await os.GetOpremaByBrojSobe(brSobe);
            return Ok(mapper.Map<List<OpremaResponseDTO>>(oprema));
        }

        [HttpGet("bySobaId/{idSobe}")]
        public async Task<IActionResult> GetOpremaByIdSobe([FromRoute] int idSobe)
        {
            var oprema = await os.GetOpremaByIdSobe(idSobe);
            return Ok(mapper.Map<List<OpremaResponseDTO>>(oprema));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOprema([FromBody] AddOpremaDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }

            if (int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }

            if (await os.SifraTaken(request.sifra))
            {
                return BadRequest("Sifra je zauzeta");
            }

            if (request == null)
            {
                return BadRequest("Los zahtev");
            }

            var oprema = mapper.Map<Oprema>(request);
            var createdOprema = await os.CreateOprema(oprema);
            return Ok(createdOprema);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOprema([FromRoute] int id, [FromBody] UpdateOpremaDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }

            if (int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }


            var oprema = await os.GetById(id);
            if (await os.SifraTaken(request.sifra) && oprema.Id != id)
            {
                return BadRequest("Sifra je zauzeta");
            }

            if (request == null)
            {
                return BadRequest("Los zahtev");
            }

            if (oprema == null)
            {
                return BadRequest("Oprema ne postoji");
            }

            var opremaUpdated = mapper.Map(request, oprema);
            await os.UpdateOprema(opremaUpdated);
            return Ok(mapper.Map<OpremaResponseDTO>(opremaUpdated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOprema([FromRoute] int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }

            if (int.Parse(userRole) != (int)UserRole.Admin)
            {
                return Forbid();
            }


            await os.DeleteOprema(id);
            return NoContent();
        }

        [HttpGet("sifraTaken/{sifra}")]
        public async Task<IActionResult> SifraProvera([FromRoute] string sifra)
        {
            if (sifra == null)
            {
                return BadRequest();
            }

            var isTaken = await os.SifraTaken(sifra);
            return Ok(isTaken);
        }
    }
}
