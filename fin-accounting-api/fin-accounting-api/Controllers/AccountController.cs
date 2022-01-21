using fin_accounting_api.Areas.Identity.Data;
using fin_accounting_api.Data;
using fin_accounting_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fin_accounting_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<apiUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly fin_accounting_apiContext _context;

        public AccountController(
             UserManager<apiUser> userManager,
             RoleManager<IdentityRole> roleManager,
             fin_accounting_apiContext context,
             IConfiguration configuration
         )
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        [Route("Test")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Test(int i)
        {
            return Ok(i);
        }

        [Route("Login")] // /login
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(!_roleManager.Roles.Any())
            {
                var res = await _roleManager.CreateAsync(new IdentityRole{ Name = "User" });

                var nUser = new apiUser
                {
                    Email = "asd@gmail.com",
                    UserName = "asd@gmail.com"
                };

                await _userManager.CreateAsync(nUser, "qwerty1234!");

                await _userManager.AddToRoleAsync(nUser, "User");
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
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

                return Ok(
                  new
                  {
                      access_token = new JwtSecurityTokenHandler().WriteToken(token),
                      userName = model.Email,
                      expiration = token.ValidTo
                  });
            }
            return Unauthorized();
        }
    }
}
