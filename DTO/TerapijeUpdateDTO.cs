namespace IS_server.DTO
{
    public class TerapijeUpdateDTO
    {
        public int id { get; set; }
        public string sifra { get; set; }
        public string sadrzaj { get; set; }
        public int brojSesija { get; set; }
        public string datumPocetka { get; set; }
        public string datumKraja { get; set; }
    }
}
