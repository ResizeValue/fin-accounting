using fin_accounting_api.Domain.Resources;

namespace fin_accounting_api.Application.Resources
{
    public interface IResourceRepository
    {
        public Task<ICollection<UserResource>> GetResourcesForUser(string userId);
        public Task<UserResource> GetResourceById(int id);
        public Task AddOwnershipCostToResourceAsync(Domain.Resources.OwnershipCost cost);
        public Task AddResource(UserResource resource);
        public Task DeleteResource(UserResource resource);
        public Task UpdateResource(UserResource resource);
    }
}
