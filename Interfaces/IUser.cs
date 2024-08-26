using IS_server.Data;

namespace IS_server.Interfaces
{
    public interface IUser
    {
        Task<User?>? GetUserById(int id);
        Task<User?>? DeleteUser(int id);
        Task<User?>? CreateUser(User user);
        Task<User?>? UpdateUser(User user);
        Task<User?>? GetUserByUserName(string userName);
        Task<User?>? DeleteByUserName(string userName);
        Task<List<User>?>? GetAllUsers();
        string GenerateToken(User user);
        string HashPassword(string password);
        bool TakenUsername(string username);
        bool TakenPassword(string password);
        Task<List<User>?>? GetUsersByRole(int role);
        Task<bool> PromoteUser(int id);
        Task<bool> DemoteUser(int id);
    }
}
