using IS_server.Data;
using IS_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IS_server.Services
{
    public class MessagesService : IMessage
    {
        private readonly DatabaseContext databaseContext;

        public MessagesService(DatabaseContext db)
        {
            this.databaseContext = db;
        }


        public async Task<Message> CreateMessage(Message message)
        {
            databaseContext.Messages.Add(message);
            await databaseContext.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteMessagesByUser(int userId)
        {
            List<Message> messages = await databaseContext.Messages.Where( message => message.receiverId == userId || message.senderId == userId).ToListAsync();
            databaseContext.Messages.RemoveRange(messages);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Message>> GetAllMessages()
        {
            return await databaseContext.Messages.ToListAsync(); }

        public async Task<List<Message>> GetAllMessagesByReceiver(int id)
        {
            return await databaseContext.Messages.Where( message => message.receiverId == id).ToListAsync();
        }

        public async Task<List<Message>> GetAllMessagesByUser(int userId)
        {
            return await databaseContext.Messages.Where( message => message.receiverId == userId || message.senderId == userId).ToListAsync();
        }

        public async Task<List<Message>> GetMessagesBySender(int id)
        {
            return await databaseContext.Messages.Where( message => message.senderId == id).ToListAsync();
        }

        public async Task<bool> ReadMessage(int id)
        {
            Message? message = await databaseContext.Messages.Where( message => message.Id == id).FirstOrDefaultAsync();
            if(message == null)
            {
                return false;
            }
            else
            {
                message.status = 1;
                await databaseContext.SaveChangesAsync();
                return true;
            }


        }
    }
}
