using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Equipment
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string lastMaintenance { get; set; }
        [ForeignKey(nameof(room))]
        public int? roomId { get; set; }
        public Room? room { get; set; }
    }
}
