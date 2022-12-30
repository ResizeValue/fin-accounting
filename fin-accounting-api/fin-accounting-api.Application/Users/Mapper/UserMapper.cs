﻿using fin_accounting_api.Application.Resources.Mapper;
using fin_accounting_api.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fin_accounting_api.Application.Users.Mapper
{
    public class UserMapper
    {
        public static UserModel GetUserModel(ApiUser user)
        {
            if(user == null)
            {
                return null;
            }

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
