namespace IS_server.DTO
{
    public class SessionResponseDTO
    {
        public int Id { get; set; }
        public int therapyId { get; set; }
        public int roomId { get; set; }
        public string dateTime { get; set; }
        public TherapyResponseDTO therapy { get; set; }
        public RoomResponseDTO room { get; set; }
    }
}
