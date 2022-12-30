using fin_accounting_api.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fin_accounting_api.Application.Users
{
    public interface IUserRepository
    {
        public Task<ApiUser> GetUserById(string id);
    }
}
