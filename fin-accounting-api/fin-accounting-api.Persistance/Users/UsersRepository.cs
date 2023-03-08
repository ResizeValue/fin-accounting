using FinAccountingApi.Application.Users;
using FinAccountingApi.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccountingApi.Persistance.Users
{
    public class UsersRepository : IUserRepository
    {
        private readonly FinAccountingApiContext _apiContext;
        public UsersRepository(FinAccountingApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        public async Task<ApiUser> GetUserById(string id)
        {
            return await _apiContext.Users.Include(user => user.Resources).FirstAsync(user => user.Id == id);
        }
    }
}
