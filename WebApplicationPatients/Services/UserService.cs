using Microsoft.AspNetCore.Mvc;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;

namespace WebApplicationPatient.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _userRepository.GetUserByUserName(userName);
            return user;
        }

        public async Task<int> AddUser(User user)
        {
            var id = await _userRepository.AddUser(user);

            return id;
        }
    }
}
