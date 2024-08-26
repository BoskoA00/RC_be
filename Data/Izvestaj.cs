using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Izvestaj
    {
        public int Id { get; set; }
        public string sifra { get; set; }
        public string sadrzaj { get; set; }
        public string vremeStvaranja { get; set; }
        [ForeignKey(nameof(Pacijent))]
        public int idPacijenta { get; set; }
        public User Pacijent { get; set; }
        [ForeignKey(nameof(Doktor))]
        public int idDoktora { get; set; }
        public User Doktor { get; set; }
    }
}
