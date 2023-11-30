using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Domain;
using Ecommerce.Model.Token.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        public JwtSettings _jwtSettings;

        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(JwtSettings jwtSettings, IHttpContextAccessor contextAccessor)
        {
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public string createToken(User user, IList<string>? roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim("userId", user.Id),
                new Claim("email", user.Email !)

            };

            foreach (var rol in roles)
            {
                var claim = new Claim(ClaimTypes.Role, rol);
                claims.Add(claim);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public string GetSessionUser()
        {
           var username =  _contextAccessor.HttpContext!.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return username!;
        }
    }
}
