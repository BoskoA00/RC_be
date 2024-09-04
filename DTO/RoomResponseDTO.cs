namespace IS_server.DTO
{
    public class RoomResponseDTO
    {
        public int Id { get; set; }
        public string roomNumber { get; set; }
        public int status { get; set; }
        public EquipmentReducedDTO equipment { get; set; }
    }
}
