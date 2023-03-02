using Microsoft.EntityFrameworkCore;
using WebApplicationPatient.Interfaces;
using WebApplicationPatient.Models;
using WebApplicationPatients.Context;

namespace WebApplicationPatient.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DefaultContext _context;

        public UserRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<int> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

    }
}
