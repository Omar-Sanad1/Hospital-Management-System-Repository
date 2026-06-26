using Core.Entities;
using HospitalManagementSystem.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalManagementSystem.CreateTokenRequest
{
    public class TokenService : ITokenService
    {
        private readonly JWT _jwt;
        public TokenService(IOptions<JWT> jwt)
        {
            _jwt = jwt.Value;
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var claims = new[]
            {
                new Claim("UserID",user.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier , user.ID.ToString()),
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.EmailAddress),
                new Claim(ClaimTypes.Role , user.Role.RoleName)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            var signingCrediantls = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer:_jwt.Issuer,
                    audience:_jwt.Audience,
                    signingCredentials:signingCrediantls,
                    claims:claims,
                    expires:DateTime.Now.AddDays(_jwt.DurationInDays)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
