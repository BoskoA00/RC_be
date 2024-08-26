namespace IS_server.DTO
{
    public class IzvestajResponseDTO
    {
        public int Id { get; set; }
        public string sadrzaj { get; set; }
        public string sifra { get; set; }
        public int idPacijenta { get; set; }
        public int idDoktora { get; set; }
        public string vremeStvaranja { get; set; }
        public UserResponseDTO Doktor { get; set; }
        public UserResponseDTO Pacijent { get; set; }
    }
}
