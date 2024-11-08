using LMS.Data.Entities;
namespace LMS.Data.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        Task CreateUser(User user);     
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid userId);
        Task UpdateUser(User user);
        Task CheckUserExist(string userPhoneNumber);
        Task<User> GetUserByPhoneNumber(string phoneNumber);    
        Task BlockUser(Guid userId);
    }
}