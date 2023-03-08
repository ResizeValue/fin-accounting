using FinAccountingApi.Domain.Users;

namespace FinAccountingApi.Domain.PaymentCheck
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<Tag>? SimilarTags { get; set; }

        public ApiUser User { get; set; }
    }
}
