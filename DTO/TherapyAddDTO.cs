namespace IS_server.DTO
{
    public class TherapyAddDTO
    {
        public string code { get; set; }
        public string content { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int sessionsNumber { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
    }
}
