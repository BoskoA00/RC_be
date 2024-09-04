using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Report
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string content { get; set; }
        public string creationTime { get; set; }
        [ForeignKey(nameof(Patient))]
        public int patientId { get; set; }
        public User Patient { get; set; }
        [ForeignKey(nameof(Doctor))]
        public int doctorId { get; set; }
        public User Doctor { get; set; }
    }
}
