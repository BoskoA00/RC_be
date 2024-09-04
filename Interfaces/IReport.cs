using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IReport
    {
        Task<Report?>? CreateReport(Report report);
        Task<List<Report>?>? GetAll();
        Task<Report?>? UpdateReport(Report report);
        Task<Report?>? DeleteById(int id);
        Task<Report?>? GetById(int id);
        Task<Report?> GetReportByCode(string code);
        Task<List<Report>?>? GetReportsByDoctor(int userId);
        Task<List<Report>?>? GetReportsByPatient(int userId);
        Task<Report?>? DeleteByCode(string code);
        bool CodeTaken(string code);
        Task<List<Report>?>? DeleteByDoctor(int doctorId);
        Task<List<Report>?>? DeleteByPatient(int patientId);
    }
}
