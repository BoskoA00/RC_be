using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface ITherapy
    {
        Task<List<Therapy>?>? GetAll();
        Task<Therapy?>? GetTherapyById(int id);
        Task<Therapy?>? CreateTherapy(Therapy therapy);
        Task<Therapy?>? UpdateTherapy(Therapy therapy);
        Task<Therapy?>? DeleteTherapyById(int id);
        Task<Therapy?>? GetTherapyByCode(string code);
        bool CodeTaken(string code);
        Task<List<Therapy>?>? GetTherapiesByPatientId(int userId);
        Task<List<Therapy>?>? GetTherapiesByDoctorId(int userId);
        Task<Therapy?>? DeleteTherapyByCode(string code);
    }
}
