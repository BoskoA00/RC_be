using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Session
    {
        public int Id { get; set; }
        [ForeignKey(nameof(room))]
        public int roomId { get; set; }
        public Room room { get; set; }
        [ForeignKey(nameof(therapy))]
        public int therapyId { get; set; }
        public Therapy therapy { get; set; }
        public string dateTime { get; set; }
    }

}
