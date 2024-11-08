using LMS.Common.Constants;
using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.JwtToken;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
namespace LMS.Service.Api
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;
        public UserService(IUserRepository userRepository, JwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task Register(UserRegisterModel userRegisterModel)
        {
            try
            {
                await _userRepository.CheckUserExist(userPhoneNumber: userRegisterModel.PhoneNumber);

                var newUser = new User
                {
                    FirstName = userRegisterModel.FirstName,
                    LastName = userRegisterModel.LastName,
                    PhoneNumber = userRegisterModel.PhoneNumber,
                    PasswordHash = userRegisterModel.Password,
                    ConfirmPasswod = userRegisterModel.ConfirmPassword,
                    Role = Constants.Client
                };
                bool isValid = IsValidPhoneNumber(userRegisterModel.PhoneNumber);
                if (isValid)
                {
                    newUser.PhoneNumber = userRegisterModel.PhoneNumber;
                }
                else
                {
                    throw new Exception("Phone number is not valid");
                }


                if (userRegisterModel.Password != null)
                {
                    var passwordHashing = new PasswordHasher<User>().HashPassword(newUser, userRegisterModel.Password);
                    newUser.PasswordHash = passwordHashing;
                }
                await _userRepository.CreateUser(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public async Task<string> Login(UserLoginModel userLoginModel)
        {
            try
            {
                var user = await _userRepository.GetUserByPhoneNumber(userLoginModel.PhoneNumber);
                var isBlock = user.IsBlocked;
                if (isBlock)
                {
                    return "User is blocked";
                }
                else
                {
                    VerfyPassword(user, userLoginModel.Password);
                    var token = _jwtTokenService.GenerateToken(user);
                    return token;

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users.ParseToDtos();
        }

        public async Task BlockUser(Guid userId)
        {
            await _userRepository.BlockUser(userId);
        }



        private bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^\+998\d{9}$|^998\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }

        private void VerfyPassword(User user, string password)
        {
            var verfyPas = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);
            if (verfyPas == PasswordVerificationResult.Failed)
            {
                throw new Exception($"The validation password is failed!");
            }
        }
        //private async Task<PasswordVerificationResult> PasswordVerification(User user, string passoword)
        //{
        //    var validationPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, passoword);
        //    if (validationPassword == PasswordVerificationResult.Failed)
        //        throw new Exception($"The validation password is failed!");
        //    return validationPassword;
        //}

    }
}

