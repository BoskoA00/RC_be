using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IEquipment
    {
        Task<List<Equipment>?>? GetAll();
        Task<Equipment?>? GetById(int id);
        Task<Equipment?>? DeleteEquipment(int id);
        Task<Equipment?>? UpdateEquipment(Equipment equipment);
        Task<Equipment?>? CreateEquipment(Equipment equipment);
        Task<Equipment?>? GetEquipmentByRoomId(int id);
        Task<Equipment?>? GetEquipmentByRoomNumber(string roomNumber);
        Task<bool> CodeTaken(string code);

    }
}
