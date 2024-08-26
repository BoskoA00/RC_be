using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Oprema
    {
        public int Id { get; set; }
        public string naziv { get; set; }
        public string sifra { get; set; }
        public string poslednjeOdrzavanje { get; set; }
        [ForeignKey(nameof(soba))]
        public int? idSobe { get; set; }
        public Soba? soba { get; set; }
    }
}
