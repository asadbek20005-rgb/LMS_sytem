using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task CheckUserExist(string userPhoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == userPhoneNumber);
            if (user != null)
                throw new SameUserExistException($"There is a user with {userPhoneNumber}, please enter another number");
        }

        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

       

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new UserNotFoundException();
            return user;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (user == null)
                throw new UserNotFoundException();
            return user;
        }

        public async Task BlockUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new UserNotFoundException();
            user.IsBlocked = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
