using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SobaController : ControllerBase
    {
        private readonly ISoba ss;
        private readonly IMapper mapper;

        public SobaController(ISoba sobaService, IMapper mp)
        {
            this.mapper = mp;
            this.ss = sobaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<SobaResponseDTO>>(await ss.GetAll()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSobaById([FromRoute] int id)
        {
            return Ok(mapper.Map<SobaResponseDTO>(await ss.GetById(id)));
        }

        [HttpGet("byBrojSobe/{brojSobe}")]
        public async Task<IActionResult> GetSobaByBr([FromRoute] string brojSobe)
        {
            return Ok(mapper.Map<SobaResponseDTO>(await ss.GetSobaByBrSobe(brojSobe)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSoba([FromBody] AddSobaDTO request)
        {
            Soba? provera = await ss.GetSobaByBrSobe(request.brojSobe);

            if (provera != null)
            {
                return BadRequest("Broj sobe je zauzet");
            }
            Soba s = await ss.CreateSoba(mapper.Map<Soba>(request));
            return Ok(s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSoba([FromRoute] int id, [FromBody] UpdateSobaDTO request)
        {
            Soba s = await ss.GetById(id);
            if (s == null)
            {
                return BadRequest("Soba ne postoji");
            }
            if (request == null)
            {
                return BadRequest();
            }
            Soba sobaUpdated = mapper.Map<UpdateSobaDTO, Soba>(request, s);
            await ss.UpdateSoba(sobaUpdated);
            return Ok(sobaUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoba([FromRoute] int id)
        {
            Soba s = await ss.DeleteSoba(id);
            if (s == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("reserve/{brSobe}")]
        public async Task<IActionResult> ReserveRoom([FromRoute] string brSobe)
        {
            if (await ss.ReserveRoom(brSobe))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("disable/{brSobe}")]
        public async Task<IActionResult> DisableRoom([FromRoute] string brSobe)
        {
            if (await ss.DisableRoom(brSobe))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("free/{brSobe}")]
        public async Task<IActionResult> FreeRoom([FromRoute] string brSobe)
        {
            if (await ss.FreeRoom(brSobe))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
