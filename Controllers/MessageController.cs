using AutoMapper;
using IS_server.Data;
using IS_server.DTO;
using IS_server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IS_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessage messageService;
        private readonly IMapper mapper;
        private readonly IUser userService;
        public MessageController (IMessage ms, IMapper mp, IUser userS)
        { 
            this.messageService = ms;
            this.mapper = mp;
            this.userService = userS;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {

            return Ok(mapper.Map<List<MessageResponseDTO>>(await messageService.GetAllMessages()));
        }
        [HttpGet("getMessagesByUser/{userId}")]
        public async Task<IActionResult> GetAllByUser([FromRoute] int userId)
        {

            return Ok(mapper.Map<List<MessageResponseDTO>>(await messageService.GetAllMessagesByUser(userId)));
        }

        [HttpGet("getMessagesBySender/{userId}")]
        public async Task<IActionResult> GetAllBySender([FromRoute] int userId)
        {

            return Ok(mapper.Map<List<MessageResponseDTO>>(await messageService.GetMessagesBySender(userId)));
        }
        
        [HttpGet("getMessagesByReceiver/{userId}")]
        public async Task<IActionResult> GetAllByReceiver([FromRoute] int userId)
        {

            return Ok(mapper.Map<List<MessageResponseDTO>>(await messageService.GetAllMessagesByReceiver(userId)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageRequestDTO messageRequestDTO)
        {
            User? sender = await userService.GetUserById(messageRequestDTO.senderId);
            if (sender == null) {
                return BadRequest();
            }
            User? receiver = await userService.GetUserById(messageRequestDTO.receiverId);
            if (receiver == null) {
                return BadRequest();
            }
            Message m = mapper.Map<Message>(messageRequestDTO);
            m.senderUserName = sender.userName;
            m.receiverUserName = receiver.userName;

            await messageService.CreateMessage(m);

            return Ok();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteMessagesByUser([FromRoute] int userId) { 
        
            bool status = await messageService.DeleteMessagesByUser(userId);
            if (status) {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ReadMessage([FromRoute] int id)
        {
            bool status = await messageService.ReadMessage(id);
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
