using fin_accounting_api.Application.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fin_accounting_api.Application.Users
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResourceModel> Resources { get; set; }
    }
}
