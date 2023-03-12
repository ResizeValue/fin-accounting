using FinAccountingApi.Application.Resources.Mapper;
using FinAccountingApi.Domain.Resources;
using FinAccountingApi.Domain.Users;

namespace FinAccountingApi.Application.Users.Mapper
{
    public class UserMapper
    {
        public static UserModel? GetUserModel(ApiUser user)
        {
            if (user == null) return null;

            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Resources = ResourceMapper.GetListResourceModel(user.Resources)
            };
        }

        public static ApiUser? GetApiUser(UserModel user)
        {
            if (user == null) return null;

            return new ApiUser
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Resources = user.Resources.Select(resource => new UserResource
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Description = resource.Description,
                    Cost = resource.Cost,
                    CreationTime = resource.CreationTime,
                    Image = resource.Image,
                }).ToList(),
            };
        }
    }
}
