using WebApplicationPatient.Models;

namespace WebApplicationPatient.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByUserName(string userName);
        Task<int> AddUser(User user);
    }
}
