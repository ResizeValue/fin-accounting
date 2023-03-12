using FinAccountingApi.Domain.Users;

namespace FinAccountingApi.Application.Users
{
    public interface IUserRepository
    {
        public Task<ApiUser> GetUserByIdAsync(string id);
    }
}
