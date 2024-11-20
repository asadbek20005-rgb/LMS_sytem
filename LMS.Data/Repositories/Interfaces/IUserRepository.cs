using LMS.Data.Entities;
namespace LMS.Data.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        Task CreateUser(User user);     
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid userId);
        Task UpdateUser(User user);
        Task CheckUserPhone(string userPhoneNumber);
        Task CheckUsername(string username);
        Task<User> GetUserByPhoneNumber(string phoneNumber);    
        Task BlockUser(Guid userId);
        Task VerifyUsername(string username);
        Task <User> GetUserByUsername(string username);
        Task CheckRoleClient(string role);
        Task CheckRoleOwner(string role);
        Task CheckRoleAdmin(string role);
        
    }
}