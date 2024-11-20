using LMS.Common.Dtos;
using LMS.Common.Models.AccountModels;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.JwtToken;
using Microsoft.AspNetCore.Identity;
    
namespace LMS.Service.Api
{
    public class AdminService(JwtTokenService jwtTokenService, ContentServce contentServce, IUserRepository userRepository, ICourseRepository courseRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ContentServce _contentService = contentServce;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;
        public async Task<string> Login(AdminLoginModel adminLoginModel)
        { 
            try
            {
                var user = await _userRepository.GetUserByUsername(adminLoginModel.Username);
                await IsAdminRole(user);
                VerifyUsername(user);
                VerfyPassword(user, adminLoginModel.Password);
                string token = _jwtTokenService.GenerateToken(user);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<List<UserDto>> GetAllUsers()
        {
            try
            {

                var users = await _userRepository.GetAllUsers();
                return users.ParseToDtos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CourseDto>> GetAllCourses()
        {
            try
            {

                var courses = await _courseRepository.GetAllCourses();
                return courses.ParseToDtos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task BlockUser(Guid userId)
        {
            try
            {

                await _userRepository.BlockUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                bool result = _contentService.DeleteFile(fileName);
                return result;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private static void VerfyPassword(User user, string password)
        {
            var verfyPas = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);
            if (verfyPas == PasswordVerificationResult.Failed)
            {
                throw new Exception($"The validation password is failed!");

            }
        }

        private async void VerifyUsername(User user)
        {
            if (!string.IsNullOrEmpty(user.Username))
            {

                await _userRepository.VerifyUsername(username: user.Username);
            }
        }


        private async Task IsAdminRole(User user)
        {
            await _userRepository.CheckRoleAdmin(user.Role);
        }
    }
}
