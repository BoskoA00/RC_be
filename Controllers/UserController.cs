using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace IS_server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser userService;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(IUser us, IMapper _mapper, IWebHostEnvironment _webHostEnvironment)
        {
            this.userService = us;
            this.mapper = _mapper;
            this.webHostEnvironment = _webHostEnvironment;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO request) {

            User? user = await userService.GetUserByUserName(request.userName);
            if (user == null)
            {
                return BadRequest("Ne postoji korisnik sa ovim korisnickim imenom");    
            }
            if( user.password != userService.HashPassword(request.password))
            {
                return BadRequest("Neodgovarajuca lozinka");
            }

            var token = userService.GenerateToken(user);

            return Ok(new {
                token = token,
                korisnik = mapper.Map<UserResponseDTO>(user)
            });

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(mapper.Map<List<UserResponseDTO>?>(await userService.GetAllUsers()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            return Ok(mapper.Map<UserResponseDTO>(await userService.GetUserById(id)));
        }

        [HttpGet("username/{userName}")]
        public async Task<IActionResult> GetUserByUserName([FromRoute] string userName)
        {
            return Ok(mapper.Map<UserResponseDTO>(await userService.GetUserByUserName(userName)));
        }
        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetUsersByRole([FromRoute] int role)
        {
            return Ok(mapper.Map<List<UserResponseDTO>>(await userService.GetUsersByRole(role)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterDTO request)
        {
            bool usernameTaken = userService.TakenUsername(request.userName);
            bool passwordTaken = userService.TakenPassword(request.password);
            if (usernameTaken)
            {
                return BadRequest("Korisničko ime je zauzeto");
            }
            if (passwordTaken)
            {
                return BadRequest("Lozinka je zauzeta");
            }
            request.password = userService.HashPassword(request.password);
            User user = mapper.Map<User>(request);

            await userService.CreateUser(user);

            return Ok(mapper.Map<UserResponseDTO>(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequestDTO request)
        {
            
            var existingUser = await userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }
          

            existingUser.firstName = request.firstName;
            existingUser.lastName = request.lastName;
            existingUser.userName = request.userName;
            existingUser.password = request.password;
            existingUser.role = request.role;
            existingUser.kontakt = request.kontakt;
          

            await userService.UpdateUser(existingUser);

            return Ok(mapper.Map<UserResponseDTO>(existingUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
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

            if (userRoleValue != (int)UserRole.Admin)
            {
                return Forbid();
            }

            var existingUser = await userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("Korisnik nije pronadjen");
            }

            await userService.DeleteUser(id);
            return NoContent();
        }


        [HttpPut("promoteUser/{id}")]
        public async Task<IActionResult> PromoteUser([FromRoute] int id)
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

            if (userRoleValue != (int)UserRole.Admin)
            {
                return Forbid();
            }


            bool status = await userService.PromoteUser(id);
            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("demoteUser/{id}")]
        public async Task<IActionResult> DemoteUser([FromRoute] int id)
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

            if (userRoleValue != (int)UserRole.Admin)
            {
                return Forbid();
            }

            bool status = await userService.DemoteUser(id);
            if (status)
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
