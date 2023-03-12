using FinAccountingApi.Application.Users;
using FinAccountingApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FinAccountingApi.Persistance.Users
{
    public class UsersRepository : IUserRepository
    {
        private readonly FinAccountingApiContext _apiContext;
        public UsersRepository(FinAccountingApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        public async Task<ApiUser> GetUserByIdAsync(string id)
        {
            return await _apiContext.Users.Include(user => user.Resources).FirstAsync(user => user.Id == id);
        }
    }
}
