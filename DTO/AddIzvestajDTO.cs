namespace IS_server.DTO
{
    public class AddIzvestajDTO
    {
        public string sadrzaj { get; set; }
        public string sifra { get; set; }
        public int idPacijenta { get; set; }
        public int idDoktora { get; set; }
        public string vremeStvaranja { get; set; }
    }
}
