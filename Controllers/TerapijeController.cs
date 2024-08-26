using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using IS_server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            Terapija? terapija = await ts.GetTerapijaById(request.id);
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
            if (await ts.GetTerapijaById(id) == null)
            {
                return NotFound("Ne postoji trazena terapija");
            }
            await ts.DeleteTerapijaById(id);
            return Ok("Terapija je izbrisana");
        }

        [HttpDelete("code/{code}")]
        public async Task<IActionResult> DeleteByCode([FromRoute] string code)
        {
            if (await ts.GetTerapijaByCode(code) == null)
            {
                return NotFound("Ne postoji trazena terapija");
            }
            await ts.DeleteTerapijaByCode(code);
            return Ok("Terapija je izbrisana");
        }
    }
}
