using fin_accounting_api.Application.Users;
using fin_accounting_api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fin_accounting_api.Persistance.Users
{
    public class UsersRepository : IUserRepository
    {
        private readonly Fin_accounting_apiContext _apiContext;
        public UsersRepository(Fin_accounting_apiContext apiContext)
        {
            _apiContext = apiContext;
        }
        public async Task<ApiUser> GetUserById(string id)
        {
            return await _apiContext.Users.Include(user => user.Resources).FirstAsync(user => user.Id == id);
        }
    }
}
