using IS_server.Data;
using System.Net;

namespace IS_server.Interfaces
{
    public interface ISesija
    {
        Task<Sesija?> GetSesijaById(int id);
        Task<List<Sesija>?>? GetAllSesije();
        Task<List<Sesija>?>? GetSesijeByTerapija(int terapijaId);
        Task<Sesija?>? CreateSesija(Sesija sesija);
        Task<Sesija?>? UpdateSesija(Sesija sesija);
        Task<Sesija?>? DeleteSesija(int id);
        Task<List<Sesija>?>? GetSesijaByTerapijaCode(string code);
        Task<bool> DeleteSesijeByTerapija(int idTerapije);
        Task<List<Sesija>?> GetSesijeByIdPacijent(int id);
        Task<List<Sesija?>?> GetSesijaByIdDoktora(int id);
    }
}
