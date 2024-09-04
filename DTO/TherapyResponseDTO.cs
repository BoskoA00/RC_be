namespace IS_server.DTO
{
    public class TherapyResponseDTO
    {
        public int id { get; set; }
        public string code { get; set; }
        public string content { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int sessionsNumber { get; set; }
        public UserResponseDTO doctor { get; set; }
        public UserResponseDTO patient { get; set; }
    }
}
