using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Soba
    {
        public int Id { get; set; }
        public string brojSobe { get; set; }
        public int status { get; set; }
        public Oprema oprema { get; set; }
    }
}
