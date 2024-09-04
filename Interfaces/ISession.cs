using IS_server.Data;
using System.Net;

namespace IS_server.Interfaces
{
    public interface ISession
    {
        Task<Session?> GetSessionById(int id);
        Task<List<Session>?>? GetAllSessions();
        Task<List<Session>?>? GetSessionsByTherapyId(int therapyId);
        Task<Session?>? CreateSession(Session session);
        Task<Session?>? UpdateSession(Session session);
        Task<Session?>? DeleteSession(int id);
        Task<List<Session>?>? GetSessionsByTherapyCode(string code);
        Task<bool> DeleteSessionsByTherapy(int therapyId);
        Task<List<Session>?> GetSessionsByPatientId(int id);
        Task<List<Session?>?> GetSessionsByDoctorId(int id);
    }
}
