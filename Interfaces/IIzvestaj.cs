using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IIzvestaj
    {
        Task<Izvestaj?>? CreateIzvestaj(Izvestaj izvestaj);
        Task<List<Izvestaj>?>? GetAll();
        Task<Izvestaj?>? UpdateIzvestaj(Izvestaj izvestaj);
        Task<Izvestaj?>? DeleteById(int id);
        Task<Izvestaj?>? GetById(int id);
        Task<Izvestaj?> GetIzvestajByCode(string code);
        Task<List<Izvestaj>?>? GetIzvestajiByDoktor(int userId);
        Task<List<Izvestaj>?>? GetIzvestajiByPacijent(int userId);
        Task<Izvestaj?>? DeleteByCode(string code);
        bool CodeTaken(string code);
        Task<List<Izvestaj>?>? DeleteByDoktor(int idDoktora);
        Task<List<Izvestaj>?>? DeleteByPacijent(int idPacijenta);
    }
}
