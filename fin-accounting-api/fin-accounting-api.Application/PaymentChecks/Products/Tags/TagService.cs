namespace FinAccountingApi.Application.PaymentChecks.Products.Tags
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<TagModel>?> GetTagsForUser(string userId) 
        {
            var tags = await _tagRepository.GetTagsForUserAsync(userId);

            return TagMapper.GetListTagModel(tags);
        }
    }
}
