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
            databaseContext.Poruke.Add(message);
            await databaseContext.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteMessagesByUser(int userId)
        {
            List<Message> messages = await databaseContext.Poruke.Where(m => m.receiverId == userId || m.senderId == userId).ToListAsync();
            databaseContext.Poruke.RemoveRange(messages);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Message>> GetAllMessages()
        {
            return await databaseContext.Poruke.ToListAsync(); }

        public async Task<List<Message>> GetAllMessagesByReceiver(int id)
        {
            return await databaseContext.Poruke.Where(m => m.receiverId == id).ToListAsync();
        }

        public async Task<List<Message>> GetAllMessagesByUser(int userId)
        {
            return await databaseContext.Poruke.Where(m => m.receiverId == userId || m.senderId == userId).ToListAsync();
        }

        public async Task<List<Message>> GetMessagesBySender(int id)
        {
            return await databaseContext.Poruke.Where(m => m.senderId == id).ToListAsync();
        }

        public async Task<bool> ReadMessage(int id)
        {
            Message? m = await databaseContext.Poruke.Where( m => m.Id == id).FirstOrDefaultAsync();
            if(m == null)
            {
                return false;
            }
            else
            {
                m.status = 1;
                await databaseContext.SaveChangesAsync();
                return true;
            }


        }
    }
}
