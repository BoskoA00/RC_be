namespace IS_server.DTO
{
    public class MessageResponseDTO
    {
        public int Id { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string senderUserName { get; set; }
        public string receiverUserName { get; set; }
        public string Content { get; set; }
        public int status { get; set; }
    }
}
