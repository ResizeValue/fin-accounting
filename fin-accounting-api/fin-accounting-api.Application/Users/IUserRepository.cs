using FinAccountingApi.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinAccountingApi.Application.Users
{
    public interface IUserRepository
    {
        public Task<ApiUser> GetUserById(string id);
    }
}
