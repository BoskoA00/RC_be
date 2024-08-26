using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IMessage
    {
        public Task<Message> CreateMessage(Message message);
        public Task<List<Message>> GetAllMessages();
        public Task<List<Message>> GetMessagesBySender(int id);
        public Task<List<Message>> GetAllMessagesByReceiver(int id);
        public Task<List<Message>> GetAllMessagesByUser(int userId);
        public Task<bool> DeleteMessagesByUser(int userId);
        public Task<bool> ReadMessage(int id);
    }
}
