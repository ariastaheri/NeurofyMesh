using NeurofyMesh.Models;

namespace NeurofyMesh.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User? GetUserById(int UserId);
        Task<bool> UpdateUser(int id, User User);
        Task<bool> DeleteUser(int UserId);

        Task<User> CreateUser(User User, bool isHashed);
    }
}
