using FinAccountingApi.Domain.PaymentCheck;

namespace FinAccountingApi.Application.PaymentChecks
{
    public interface IPaymentCheckRepository
    {
        public Task<IReadOnlyCollection<PaymentCheck>> GetPaymentChecksForUserAsync(string userId);

        public Task<PaymentCheck?> GetPaymentCheckByIdAsync(string id);

        public Task<int> GetTotalNumberOfPaymentsForUserAsync(string userId);

        public Task<Guid> AddPaymentCheckAsync(PaymentCheck paymentCheck);

        public Task UpdatePaymentCheckAsync(PaymentCheck paymentCheck);

        public Task DeletePaymentCheckAsync(PaymentCheck paymentCheck);
    }
}
