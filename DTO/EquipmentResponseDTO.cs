namespace IS_server.DTO
{
    public class EquipmentResponseDTO
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int roomId { get; set; }
        public string lastMaintenance { get; set; }
        public RoomResponseDTO room { get; set; }
    }
}
