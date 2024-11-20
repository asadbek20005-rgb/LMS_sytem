using LMS.Common.Constants;
using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.JwtToken;
using LMS.Service.MemoryCache;
using LMS.Service.Otp;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace LMS.Service.Api
{
    public class OwnerService(IOtpRepository otpRepository, MemoryCacheService memoryCacheService, IUserRepository userRepository, JwtTokenService jwtTokenService, OtpService otpService)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;
        private readonly OtpService _otpService = otpService;
        private readonly MemoryCacheService _memoryCacheService = memoryCacheService;
        private readonly IOtpRepository _otpRepository = otpRepository;
        public async Task<int> Register(OwnerRegisterModel userRegisterModel)
        {

            try
            {
                await _userRepository.CheckUserPhone(userPhoneNumber: userRegisterModel.PhoneNumber);
                await _userRepository.CheckUsername(username: userRegisterModel.Username);


                var newUser = new User
                {
                    FirstName = userRegisterModel.FirstName,
                    LastName = userRegisterModel.LastName,
                    Username = userRegisterModel.Username,
                    Role = Constants.Owner
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
                _memoryCacheService.SetEntity(newUser);
                int code = _otpService.GenerateCode(newUser.PhoneNumber);
                return code;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> VerifyRegister(OtpModel otpModel)
        {
            try
            {
                await _otpService.CheckCodeExpired(otpModel.Code);
                var code = (int)_otpService.VerifyRegister(otpModel);


                if (code != otpModel.Code)
                    throw new Exception("The code is not valid");


                var newOtp = new OTP
                {
                    Code = otpModel.Code,
                    PhoneNumber = otpModel.PhoneNumber,
                    IsExpired = true
                };

                await _otpRepository.Create(newOtp);

                var value = _memoryCacheService.GetEntity(Constants.CacheOwnerKey);

                if (value == null)
                {
                    throw new Exception();
                }


                var owner = (User)value;
                await _userRepository.CreateUser(owner);
                return Constants.RegisterSuccessfull;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }





        public async Task<int> Login(OwnerLoginModel userLoginModel)
        {
            try
            {
                var user = await _userRepository.GetUserByPhoneNumber(userLoginModel.PhoneNumber);
                await IsOwnerRole(user);
                IsBlocked(user);
                VerifyUsername(user);
                VerfyPassword(user, userLoginModel.Password);

                _memoryCacheService.SetEntity(user);
                int code = _otpService.GenerateCode(phoneNumber: userLoginModel.PhoneNumber);
                return code;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> VerifyLogin(OtpModel otpModel)
        {
            try
            {
                await _otpService.CheckCodeExpired(otpModel.Code);
                var code = (int)_otpService.VerifyRegister(otpModel);
                if (code != otpModel.Code)
                    throw new Exception("The code is not valid");

                var newOtp = new OTP
                {
                    Code = otpModel.Code,
                    PhoneNumber = otpModel.PhoneNumber,
                    IsExpired = true
                };


                await _otpRepository.Create(newOtp);

                var value = _memoryCacheService.GetEntity(Constants.CacheOwnerKey);

                if (value == null)
                {
                    throw new Exception();
                }


                var owner = (User)value;
                return _jwtTokenService.GenerateToken(owner);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        private static void IsBlocked(User user)
        {
            if (user.IsBlocked)
                throw new Data.Exceptions.User.UserBlockedException();
        }

        private static bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^\+998\d{9}$|^998\d{9}$";
            return Regex.IsMatch(phone, pattern);
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


        private async Task IsOwnerRole(User user)
        {
            await _userRepository.CheckRoleOwner(user.Role);
        }
    }
}
