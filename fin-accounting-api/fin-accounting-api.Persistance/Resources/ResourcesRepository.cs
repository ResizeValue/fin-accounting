using FinAccountingApi.Application.Resources;
using FinAccountingApi.Domain.Resources;
using Microsoft.EntityFrameworkCore;

namespace FinAccountingApi.Persistance.Resources
{
    public class ResourcesRepository : IResourceRepository
    {
        private readonly FinAccountingApiContext _apiContext;

        public ResourcesRepository(FinAccountingApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task AddResource(UserResource resource)
        {
            await _apiContext.AddAsync(resource);
            await _apiContext.SaveChangesAsync();
        }

        public async Task<ICollection<UserResource>> GetResourcesForUser(string userId)
        {
            return await _apiContext.UserResources
                .Where(res => res.Owner.Id == userId && res.UserResourceId == null)
                .Include(res => res.Resources)
                .Include(res => res.OwnershipCost)
                .ToListAsync();
        }

        public async Task<UserResource> GetResourceById(int resourceId)
        {
            return await _apiContext.UserResources
                .Include(res => res.Resources)
                .Include(res => res.OwnershipCost)
                .FirstAsync(res => res.Id == resourceId);
                
        }

        public async Task AddOwnershipCostToResourceAsync(OwnershipCost cost)
        {
            await _apiContext.OwnershipCost.AddAsync(cost);
            await _apiContext.SaveChangesAsync();
        }

        public async Task UpdateResource(UserResource resource)
        {
            _apiContext.UserResources.Update(resource);
            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteResource(UserResource resource)
        {
            _apiContext.UserResources.Remove(resource);
            await _apiContext.SaveChangesAsync();
        }
    }
}
