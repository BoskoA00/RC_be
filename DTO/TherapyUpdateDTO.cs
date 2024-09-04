namespace IS_server.DTO
{
    public class TherapyUpdateDTO
    {
        public int id { get; set; }
        public string code { get; set; }
        public string content { get; set; }
        public int sessionsNumber { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
