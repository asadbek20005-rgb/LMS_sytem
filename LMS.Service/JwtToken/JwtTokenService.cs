using LMS.Common.JwtModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Service.JwtToken
{
    public class JwtTokenService
    {

        private readonly IConfiguration _configuration;
        private readonly JwtSetting _jwtSetting;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSetting = _configuration.GetSection("JwtSetting").Get<JwtSetting>()!;
        }


        public string GenerateToken(Data.Entities.User user)
        {

           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.Role)
            };


            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                signingCredentials: cred,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
