using LMS.Common.Constants;
using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.User;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LMS.Data.Repositories.Implementations
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task CheckUserPhone(string userPhoneNumber)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == userPhoneNumber);
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
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new UserNotFoundException();
            return user;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) ?? throw new UserNotFoundException();
            return user;
        }

        public async Task BlockUser(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new UserNotFoundException();
            user.IsBlocked = true;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username) ?? throw new UserNotFoundException();
            return user;
        }

        public async Task CheckUsername(string username)
        {
            var users = _context.Users;
            if (users is null)
                throw new UserNotFoundException();

            var haveUsername = await users.AsNoTracking().AnyAsync(x => x.Username == username);
            if (haveUsername)
                throw new SameUserExistException($"User with {username} is already exist");
        }

        public async Task VerifyUsername(string username)
        {
            var users = _context.Users;
            if (users is null)
                throw new UserNotFoundException();

            var haveUsername = await users.AsNoTracking().AnyAsync(x => x.Username == username);
            if (!haveUsername)
                throw new UsernameNotVerified();
        }

        public async Task CheckRoleClient(string role)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Role == role);

            if (user is null)
                throw new UserNotFoundException();

            if(user.Role != Constants.Client)
                throw new RoleNotVerifyException("Role must be <<Client>>");

        }

        public async Task CheckRoleOwner(string role)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Role == role);

            if (user is null)
                throw new UserNotFoundException();

            if (user.Role != Constants.Owner)
                throw new RoleNotVerifyException("Role must be <<Owner>>");
        }

        public async Task CheckRoleAdmin(string role)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Role == role);

            if (user is null)
                throw new UserNotFoundException();

            if (user.Role != Constants.Admin)
                throw new RoleNotVerifyException("Role must be <<Admin>>");
        }
    }
}
