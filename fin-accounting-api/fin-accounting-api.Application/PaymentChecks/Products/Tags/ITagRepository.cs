using FinAccountingApi.Domain.PaymentCheck;

namespace FinAccountingApi.Application.PaymentChecks.Products.Tags
{
    public interface ITagRepository
    {
        public Task<IReadOnlyCollection<Tag>> GetTagsForUserAsync(string userId);

        public Task<Tag?> GetTagByIdAsync(int id);

        public Task<int> AddTagAsync(Tag tag);

        public Task UpdateTagAsync(Tag tag);

        public Task DeleteTagAsync(Tag tag);
    }
}
