using FinAccountingApi.Application.PaymentChecks.Products.Tags;
using FinAccountingApi.Domain.PaymentCheck;
using Microsoft.EntityFrameworkCore;

namespace FinAccountingApi.Persistance.PaymentChecks.Products.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly FinAccountingApiContext _apiContext;

        public TagRepository(FinAccountingApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IReadOnlyCollection<Tag>> GetTagsForUserAsync(string userId)
        {
            return await _apiContext.Tags
                .Include(tag => tag.User)
                .Where(tag => tag.User.Id == userId)
                .ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _apiContext.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
        }

        public async Task<int> AddTagAsync(Tag tag)
        {
            await _apiContext.AddAsync(tag);
            await _apiContext.SaveChangesAsync();

            return tag.Id;
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _apiContext.Update(tag);
            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(Tag tag)
        {
            _apiContext.Remove(tag);
            await _apiContext.SaveChangesAsync();
        }
    }
}
