using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IOprema
    {
        Task<List<Oprema>?>? GetAll();
        Task<Oprema?>? GetById(int id);
        Task<Oprema?>? DeleteOprema(int id);
        Task<Oprema?>? UpdateOprema(Oprema oprema);
        Task<Oprema?>? CreateOprema(Oprema oprema);
        Task<Oprema?>? GetOpremaByIdSobe(int id);
        Task<Oprema?>? GetOpremaByBrojSobe(string brojSobe);
        Task<bool> SifraTaken(string sifra);

    }
}
