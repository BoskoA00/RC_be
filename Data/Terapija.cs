using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Terapija
    {
        public int Id { get; set; }
        public string sifra { get; set; }
        public string  sadrzaj { get; set; }
        public string datumPocetka { get; set; }
        public string datumKraja { get; set; }
        public int brojSesija { get; set; }
        [ForeignKey(nameof(Pacijent))]
        public int idPacijenta { get; set; }
        public User Pacijent { get; set; }
        [ForeignKey(nameof(Doktor))]
        public int idDoktora { get; set; }
        public User Doktor { get; set; }

    }
}
