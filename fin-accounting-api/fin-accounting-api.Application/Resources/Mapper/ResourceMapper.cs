using FinAccountingApi.Application.Users.Mapper;
using FinAccountingApi.Domain.Resources;

namespace FinAccountingApi.Application.Resources.Mapper
{
    public class ResourceMapper
    {
        public static ResourceModel GetResourceModel(UserResource resource)
        {
            return new ResourceModel
            {
                Id = resource.Id,
                CreationTime = resource.CreationTime,
                Cost = resource.Cost,
                OwnershipCost = OwnershipCostMapper.GetOwnershipCostModelCollection(resource.OwnershipCost),
                Name = resource.Name,
                Description = resource.Description,
                Image = resource.Image,
                ParentId = resource.UserResourceId,
                Resources = GetListResourceModel(resource.Resources),
                Owner = UserMapper.GetUserModel(resource.Owner),
            };
        }

        public static IEnumerable<ResourceModel> GetListResourceModel(IEnumerable<UserResource> resource)
        {
            if (resource == null) return null;
            return resource.Select(x => GetResourceModel(x));
        }
    }
}
