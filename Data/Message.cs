using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string senderUserName { get; set; }
        public string receiverUserName { get; set; }
        public int status { get; set; } = 0;
        [ForeignKey(nameof(senderId))]
        public int senderId { get; set; }
        public User Sender { get; set; }
        [ForeignKey(nameof(receiverId))]
        public int receiverId { get; set; }
        public User Receiver { get; set; }
    }
}
