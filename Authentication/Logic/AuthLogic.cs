using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Logic
{
    public class AuthLogic : ShareLogic
    {
        private readonly IConfiguration _configuration;
        public AuthLogic(IServiceProvider provider) : base(provider)
        {
            _configuration = provider.GetRequiredService<IConfiguration>();
        }
        public async Task<string> Login(AuthModel model)
        {
            var entity = await _db.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == model.UserName.ToLower());
            if (entity == null)
            {
                throw new Exception("User Not Found!");
            }
            
            if (!VerifyPasswordHash(model.Password, entity.PasswordHash, entity.PasswordSalt))
            {
                throw new Exception("Wrong Password or Username!");
            }

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("Secret").Value));
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT").GetSection("ValidIssuer").Value,
                audience: _configuration.GetSection("JWT").GetSection("ValidAudience").Value,
                expires: DateTime.Now.AddHours(1),
                claims: authClaim,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
