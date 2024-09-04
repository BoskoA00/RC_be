using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Room
    {
        public int Id { get; set; }
        public string roomNumber { get; set; }
        public int status { get; set; }
        public Equipment equipment { get; set; }
    }
}
