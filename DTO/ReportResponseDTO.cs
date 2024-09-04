namespace IS_server.DTO
{
    public class ReportResponseDTO
    {
        public int Id { get; set; }
        public string content { get; set; }
        public string code { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public string creationTime { get; set; }
        public UserResponseDTO Doctor { get; set; }
        public UserResponseDTO Patient { get; set; }
    }
}
