using FinAccountingApi.Application.Users;

namespace FinAccountingApi.Application.PaymentChecks.Products.Tags
{
    public class TagModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<TagModel>? SimilarTags { get; set; }

        public UserModel? User { get; set; }
    }
}
