using FinAccountingApi.Domain.PaymentCheck;
using Microsoft.AspNetCore.Mvc;

namespace fin_accounting_api.UI.Controllers.PaymentChecks
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentChecks : ControllerBase
    {
        [HttpGet]
        [Route("{userId}")]
        public async Task<IEnumerable<PaymentCheck>> GetPaymentChecksForUser(string userId)
        {
            return new List<PaymentCheck>();
        }
    }
}
