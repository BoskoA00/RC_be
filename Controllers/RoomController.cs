using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoom roomService;
        private readonly IMapper mapper;

        public RoomController(IRoom rs, IMapper mp)
        {
            this.mapper = mp;
            this.roomService = rs;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<RoomResponseDTO>>(await roomService.GetAll()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoomById([FromRoute] int id)
        {
            return Ok(mapper.Map<RoomResponseDTO>(await roomService.GetById(id)));
        }

        [HttpGet("ByRoomNumber/{roomNumber}")]
        public async Task<IActionResult> GetRoomByNumber([FromRoute] string roomNubmer)
        {
            return Ok(mapper.Map<RoomResponseDTO>(await roomService.GetRoomByNumber(roomNubmer)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] AddRoomDTO request)
        {

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;


            if (userRole == null) {
                return Forbid();
            }

            if(int.Parse(userRole) != (int)UserRole.Admin )
            {
                return Forbid();
            }

            Room? provera = await roomService.GetRoomByNumber(request.roomNumber);

            if (provera != null)
            {
                return BadRequest("Room number is taken.");
            }
            Room room = await roomService.CreateRoom(mapper.Map<Room>(request));
            return Ok(room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSoba([FromRoute] int id, [FromBody] UpdateRoomDTO request)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }

            if (int.Parse(userRole) != (int)UserRole.Admin )
            {
                return Forbid();
            }

            Room room = await roomService.GetById(id);
            if (room == null)
            {
                return BadRequest("Room doesn't exist.");
            }
            if (request == null)
            {
                return BadRequest();
            }
            Room roomUpdated = mapper.Map<UpdateRoomDTO, Room>(request, room);
            await roomService.UpdateRoom(roomUpdated);
            return Ok(roomUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
            {
                return Forbid();
            }

            if (int.Parse(userRole) != (int)UserRole.Admin )
            {
                return Forbid();
            }
            Room room = await roomService.DeleteRoom(id);
            if (room == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut("reserve/{roomNumber}")]
        public async Task<IActionResult> ReserveRoom([FromRoute] string roomNumber)
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
            if (await roomService.ReserveRoom(roomNumber))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("disable/{roomNumber}")]
        public async Task<IActionResult> DisableRoom([FromRoute] string roomNumber)
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

            if (await roomService.DisableRoom(roomNumber))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("free/{roomNumber}")]
        public async Task<IActionResult> FreeRoom([FromRoute] string roomNumber)
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

            if (await roomService.FreeRoom(roomNumber))
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
