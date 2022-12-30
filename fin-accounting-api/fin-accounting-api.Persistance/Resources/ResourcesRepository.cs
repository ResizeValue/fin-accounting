using fin_accounting_api.Application.Resources;
using fin_accounting_api.Domain.Resources;
using Microsoft.EntityFrameworkCore;

namespace fin_accounting_api.Persistance.Resources
{
    public class ResourcesRepository : IResourceRepository
    {
        private readonly Fin_accounting_apiContext _apiContext;

        public ResourcesRepository(Fin_accounting_apiContext apiContext)
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
