using fin_accounting_api.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fin_accounting_api.Application.Resources.OwnershipCost
{
    public class OwnershipCostForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public OwnershipCostPeriodicityEnum Periodicity { get; set; }
        public string? Description { get; set; }
        public int ResourceId { get; set; }
    }
}
