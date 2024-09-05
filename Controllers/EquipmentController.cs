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
    public class EquipmentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEquipment equipmentService;

        public EquipmentController(IMapper _mapper, IEquipment _equipmentService)
        {
            this.mapper = _mapper;
            this.equipmentService = _equipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equipmentList = await equipmentService.GetAll();
            return Ok(mapper.Map<List<EquipmentResponseDTO>>(equipmentList));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] int id)
        {
            var equipment = await equipmentService.GetById(id);
            return Ok(mapper.Map<EquipmentResponseDTO>(equipment));
        }

        [HttpGet("ByRoomNumber/{roomNumber}")]
        public async Task<IActionResult> GetEquipmentByRoomNumber([FromRoute] string roomNumber)
        {
            var equipment = await equipmentService.GetEquipmentByRoomNumber(roomNumber);
            return Ok(mapper.Map<List<EquipmentResponseDTO>>(equipment));
        }

        [HttpGet("ByRoomId/{roomId}")]
        public async Task<IActionResult> GetEquipmentByRoomId([FromRoute] int roomId)
        {
            var equipment = await equipmentService.GetEquipmentByRoomId(roomId);
            return Ok(mapper.Map<List<EquipmentResponseDTO>>(equipment));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] AddEquipmentDTO request)
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

            if (await equipmentService.CodeTaken(request.code))
            {
                return BadRequest("Code taken.");
            }

            if (request == null)
            {
                return BadRequest("Los zahtev");
            }

            var equipment = mapper.Map<Equipment>(request);
            var createdEquipment = await equipmentService.CreateEquipment(equipment);
            return Ok(createdEquipment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment([FromRoute] int id, [FromBody] UpdateOpremaDTO request)
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


            var equipment = await equipmentService.GetById(id);
            if (await equipmentService.CodeTaken(request.code) && equipment.Id != id)
            {
                return BadRequest("Code taken.");
            }

            if (request == null)
            {
                return BadRequest("Bad request.");
            }

            if (equipment == null)
            {
                return BadRequest("Wanted equipment doesn't exist.");
            }

            var updatedEquipment = mapper.Map(request, equipment);
            await equipmentService.UpdateEquipment(updatedEquipment);
            return Ok(mapper.Map<EquipmentResponseDTO>(updatedEquipment));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment([FromRoute] int id)
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


            await equipmentService.DeleteEquipment(id);
            return NoContent();
        }

        [HttpGet("codeTaken/{code}")]
        public async Task<IActionResult> SifraProvera([FromRoute] string code)
        {
            var isTaken = await equipmentService.CodeTaken(code);
            return Ok(isTaken);
        }
    }
}
