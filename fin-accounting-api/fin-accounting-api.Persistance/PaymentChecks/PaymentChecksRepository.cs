using FinAccountingApi.Application.PaymentChecks;
using FinAccountingApi.Domain.PaymentCheck;
using Microsoft.EntityFrameworkCore;

namespace FinAccountingApi.Persistance.PaymentChecks
{
    public class PaymentChecksRepository : IPaymentCheckRepository
    {
        private readonly FinAccountingApiContext _apiContext;

        public PaymentChecksRepository(FinAccountingApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IReadOnlyCollection<PaymentCheck>> GetPaymentChecksForUserAsync(string userId)
        {
            return await _apiContext.PaymentChecks
                .Include(check => check.Products)
                .ThenInclude(product => product.Tags)
                .Where(check => check.User.Id == userId)
                .ToListAsync();
        }

        public async Task<PaymentCheck?> GetPaymentCheckByIdAsync(string id)
        {
            return await _apiContext.PaymentChecks
                .Include(check => check.Products)
                .ThenInclude(product => product.Tags)
                .FirstOrDefaultAsync(payment => payment.Id.ToString() == id);
        }

        public Task<int> GetTotalNumberOfPaymentsForUserAsync(string userId)
        {
            return _apiContext.PaymentChecks
                .Where(check => check.User.Id == userId)
                .CountAsync();
        }

        public async Task<Guid> AddPaymentCheckAsync(PaymentCheck paymentCheck)
        {
            await _apiContext.PaymentChecks.AddAsync(paymentCheck);
            await _apiContext.SaveChangesAsync();

            return paymentCheck.Id;
        }

        public async Task UpdatePaymentCheckAsync(PaymentCheck paymentCheck)
        {
            _apiContext.PaymentChecks.Update(paymentCheck);

            await _apiContext.SaveChangesAsync();
        }

        public async Task DeletePaymentCheckAsync(PaymentCheck paymentCheck)
        {
            _apiContext.PaymentChecks.Remove(paymentCheck);

            await _apiContext.SaveChangesAsync();
        }
    }
}
