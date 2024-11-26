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

namespace LMS.Service.Api;

public class ClientService(MemoryCacheService memoryCacheService, IUserRepository userRepository, JwtTokenService jwtTokenService, OtpService otpService, IOtpRepository otpRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly JwtTokenService _jwtTokenService = jwtTokenService;
    private readonly OtpService _otpService = otpService;
    private readonly IOtpRepository _otpRepository = otpRepository;
    private readonly MemoryCacheService _memoryCacheService = memoryCacheService;
    public async Task<int> Register(ClientRegisterModel userRegisterModel)
    {

        try
        {
            await _userRepository.CheckUserPhone(userPhoneNumber: userRegisterModel.PhoneNumber);
            CheckPhonenumberCache(userRegisterModel.PhoneNumber);

            var newUser = new User
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
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
            _memoryCacheService.SetEntity(Constants.CacheClientKey,newUser);
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

            var value = _memoryCacheService.GetEntity(Constants.CacheClientKey);

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



    public async Task<int> Login(ClientLoginModel userLoginModel)
    {
        try
        {
            var user = await _userRepository.GetUserByPhoneNumber(userLoginModel.PhoneNumber);
            await IsClientRole(user);
            IsBlocked(user);
            _memoryCacheService.SetEntity(Constants.CacheClientKey,user);
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
            int code = (int)_otpService.VerifyLogin(otpModel);

            if (code != otpModel.Code)
                throw new Exception("The code is not valid");

            var newOtp = new OTP
            {
                Code = otpModel.Code,
                PhoneNumber = otpModel.PhoneNumber,
                IsExpired = true
            };


            await _otpRepository.Create(newOtp);

            var value = _memoryCacheService.GetEntity(Constants.CacheClientKey);

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

    private async Task IsClientRole(User user)
    {
        await _userRepository.CheckRoleClient(user.Role);

    }

    private void CheckPhonenumberCache(string phoneNumber)
    {
        var entity = _memoryCacheService.GetEntity(Constants.CacheClientKey);


        var user = (User)entity;


        if (phoneNumber == user?.PhoneNumber)
            throw new Exception($"The phonenumber with {phoneNumber} is already exist");
    }
}
