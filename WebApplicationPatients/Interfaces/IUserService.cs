using Microsoft.AspNetCore.Mvc;
using WebApplicationPatient.Models;

namespace WebApplicationPatient.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<User> Login(string userName, string password);
        Task<int> AddUser(User user);
    }
}
