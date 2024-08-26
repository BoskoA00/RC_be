namespace IS_server.DTO
{
    public class OpremaResponseDTO
    {
        public int Id { get; set; }
        public string sifra { get; set; }
        public string naziv { get; set; }
        public int idSobe { get; set; }
        public string poslednjeOdrzavanje { get; set; }
        public SobaResponseDTO soba { get; set; }
    }
}
