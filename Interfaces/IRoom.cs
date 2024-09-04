using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IRoom
    {
        Task<Room?>? CreateRoom(Room room);
        Task<Room?>? UpdateRoom(Room room);
        Task<Room?>? DeleteRoom(int id);
        Task<Room?>? GetRoomByNumber(string number);
        Task<List<Room>?>? GetAll();
        Task<Room?>? GetById(int id);
        int CheckStatus(string roomNumber);
        Task<bool> ReserveRoom(string roomNumber);
        Task<bool> FreeRoom(string roomNumber);
        Task<bool> DisableRoom(string roomNumber);
    }
}
