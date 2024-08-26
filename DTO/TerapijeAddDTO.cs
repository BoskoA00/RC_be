namespace IS_server.DTO
{
    public class TerapijeAddDTO
    {
        public string sifra { get; set; }
        public string sadrzaj { get; set; }
        public string datumPocetka { get; set; }
        public string datumKraja { get; set; }
        public int brojSesija { get; set; }
        public int idPacijenta { get; set; }
        public int idDoktora { get; set; }
    }
}
