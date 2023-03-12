using FinAccountingApi.Domain.Resources;

namespace FinAccountingApi.Application.Resources
{
    public interface IResourceRepository
    {
        public Task<ICollection<UserResource>> GetResourcesForUserAsync(string userId);
        public Task<UserResource> GetResourceByIdAsync(int id);
        public Task AddOwnershipCostToResourceAsync(Domain.Resources.OwnershipCost cost);
        public Task AddResourceAsync(UserResource resource);
        public Task UpdateResourceAsync(UserResource resource);
        public Task DeleteResourceAsync(UserResource resource);
    }
}
