using fin_accounting_api.Application.Accounts.Auth;
using fin_accounting_api.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fin_accounting_api.Application.Accounts
{
    public class AccountsService
    {

        private readonly UserManager<ApiUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountsService(
            IConfiguration configuration,
            UserManager<ApiUser> userManager,
             RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task RegisterUser(RegisterModel model)
        {
            if (!_roleManager.Roles.Any())
            {
                var res = await _roleManager.CreateAsync(new IdentityRole { Name = "FinUser" });
            }

            var newUser = new ApiUser
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
            };

            await _userManager.CreateAsync(newUser, model.Password);

            await _userManager.AddToRoleAsync(newUser, "FinUser");
        }

        public async Task<UserToken> LoginUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id)
                };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }

            var signingKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

            int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Site"],
              audience: _configuration["Jwt:Site"],
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
              signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new UserToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Email = model.Email,
                Name = user.Name,
                Id = user.Id,
                Expiration = token.ValidTo
            };
        }
    }
}
