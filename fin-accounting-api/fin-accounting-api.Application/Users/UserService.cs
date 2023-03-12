using FinAccountingApi.Application.Resources.Mapper;
using Microsoft.Extensions.Configuration;

namespace FinAccountingApi.Application.Users
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
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) throw new KeyNotFoundException(id);

            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Resources = ResourceMapper.GetListResourceModel(user.Resources)
            };
        }
    }
}
