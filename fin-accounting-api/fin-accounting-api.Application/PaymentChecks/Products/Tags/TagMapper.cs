using FinAccountingApi.Application.Users.Mapper;
using FinAccountingApi.Domain.PaymentCheck;

namespace FinAccountingApi.Application.PaymentChecks.Products.Tags
{
    public class TagMapper
    {
        public static TagModel GetTagModel(Tag tag)
        {
            return new TagModel
            {
                Id = tag.Id,
                Name = tag.Name,
                User = UserMapper.GetUserModel(tag.User),
                SimilarTags = tag.SimilarTags?.Select(t => new TagModel
                {
                    Id = t.Id,
                    Name = t.Name,
                })
            };
        }

        public static IEnumerable<TagModel>? GetListTagModel(IEnumerable<Tag>? tags)
        {
            if (tags == null) return null;

            return tags.Select(x => GetTagModel(x));
        }

        public static Tag GetTag(TagModel tag)
        {
            return new Tag
            {
                Id = tag.Id,
                Name = tag.Name,
                User = UserMapper.GetApiUser(tag.User),
                SimilarTags = tag.SimilarTags?.Select(t => new Tag
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToList(),
            };
        }

        public static IEnumerable<Tag>? GetListTag(IEnumerable<TagModel>? tags)
        {
            if (tags == null) return null;

            return tags.Select(x => GetTag(x));
        }
    }
}
