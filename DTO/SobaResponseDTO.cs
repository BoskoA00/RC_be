namespace IS_server.DTO
{
    public class SobaResponseDTO
    {
        public int Id { get; set; }
        public string brojSobe { get; set; }
        public int status { get; set; }
        public OpremaReducedDTO oprema { get; set; }
    }
}
