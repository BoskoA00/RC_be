using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Sesija
    {
        public int Id { get; set; }
        [ForeignKey(nameof(soba))]
        public int idSobe { get; set; }
        public Soba soba { get; set; }
        [ForeignKey(nameof(terapija))]
        public int idTerapije { get; set; }
        public Terapija terapija { get; set; }
        public string termin { get; set; }
    }

}
