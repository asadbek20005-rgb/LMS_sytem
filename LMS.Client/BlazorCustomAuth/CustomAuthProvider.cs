using LMS.Client.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LMS.Client.BlazorCustomAuth
{
    public class CustomAuthProvider(LocalStorageService localStorageService, JwtSecurityTokenHandler jwtSecurityTokenHandler) : Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider
    {
        private readonly LocalStorageService _localStorageService = localStorageService;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetToken();

            if(string.IsNullOrWhiteSpace(token))
            {
                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(claimPrincipal); 
            }

            var claims = ParseClaimsFromToken(token);
            var identity = new ClaimsIdentity(claims, "jwtAuth");
            var principal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            return new AuthenticationState(principal);
        }


        private List<Claim> ParseClaimsFromToken(string token)
        {
            var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var userId = jwtSecurityToken.Claims
                     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var role = jwtSecurityToken.Claims
                    .FirstOrDefault(c =>
                    c.Type == ClaimTypes.Role)!.Value;

            var mobileNumber = jwtSecurityToken.Claims
                     .FirstOrDefault(c =>
                  c.Type == ClaimTypes.MobilePhone
                     )!.Value;

            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.NameIdentifier, userId),
               new Claim(ClaimTypes.MobilePhone, mobileNumber),
               new Claim(ClaimTypes.Role, role),

            };

            return claims;
        }
    }
}
