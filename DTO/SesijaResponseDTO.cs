namespace IS_server.DTO
{
    public class SesijaResponseDTO
    {
        public int Id { get; set; }
        public int idTerapije { get; set; }
        public int idSobe { get; set; }
        public string termin { get; set; }
        public TerapijeResponseDTO terapija { get; set; }
        public SobaResponseDTO soba { get; set; }
    }
}
