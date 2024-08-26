using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface ITerapija
    {
        Task<List<Terapija>?>? GetAll();
        Task<Terapija?>? GetTerapijaById(int id);
        Task<Terapija?>? CreateTerapija(Terapija terapija);
        Task<Terapija?>? UpdateTerapija(Terapija terapija);
        Task<Terapija?>? DeleteTerapijaById(int id);
        Task<Terapija?>? GetTerapijaByCode(string code);
        bool CodeTaken(string code);
        Task<List<Terapija>?>? GetTerapijeByPacijent(int userId);
        Task<List<Terapija>?>? GetTerapijeByDoktor(int userId);
        Task<Terapija?>? DeleteTerapijaByCode(string code);
    }
}
