namespace IS_server.DTO
{
    public class AddReportDTO
    {
        public string content { get; set; }
        public string code { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public string creationTime { get; set; }
    }
}
