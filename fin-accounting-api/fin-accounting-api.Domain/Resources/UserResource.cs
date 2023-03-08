using FinAccountingApi.Domain.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinAccountingApi.Domain.Resources
{
    public class UserResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public decimal Cost { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public ICollection<OwnershipCost> OwnershipCost { get; set; }

        [ForeignKey("UserResourceId")]
        public virtual int? UserResourceId { get; set; }

        public ICollection<UserResource> Resources { get; set; }

        public ApiUser Owner { get; set; }
    }
}
