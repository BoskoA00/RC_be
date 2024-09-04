using IS_server.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_server.Data
{
    public class Therapy
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string  content { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int sessionsNumber { get; set; }
        [ForeignKey(nameof(patient))]
        public int patientId { get; set; }
        public User patient { get; set; }
        [ForeignKey(nameof(doctor))]
        public int doctorId { get; set; }
        public User doctor { get; set; }

    }
}
