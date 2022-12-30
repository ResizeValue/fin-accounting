using fin_accounting_api.Application.Accounts.Auth;
using fin_accounting_api.Application.Resources.Mapper;
using fin_accounting_api.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fin_accounting_api.Application.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(
            IConfiguration configuration,
            IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public async Task<UserModel> GetUserProfileAsync(string id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                throw new KeyNotFoundException(id);
            }

            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Resources = ResourceMapper.GetListResourceModel(user.Resources)
            };
        }

        public Task LoginUserAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}
