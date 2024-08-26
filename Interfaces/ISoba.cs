using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface ISoba
    {
        Task<Soba?>? CreateSoba(Soba soba);
        Task<Soba?>? UpdateSoba(Soba soba);
        Task<Soba?>? DeleteSoba(int id);
        Task<Soba?>? GetSobaByBrSobe(string broj);
        Task<List<Soba>?>? GetAll();
        Task<Soba?>? GetById(int id);
        int CheckStatus(string brSobe);
        Task<bool> ReserveRoom(string brojSobe);
        Task<bool> FreeRoom(string brojSobe);
        Task<bool> DisableRoom(string brojSobe);
    }
}
