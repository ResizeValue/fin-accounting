using FinAccountingApi.Application.PaymentChecks.Products;
using FinAccountingApi.Application.PaymentChecks.Products.Tags;
using FinAccountingApi.Application.Users;

namespace FinAccountingApi.Application.PaymentChecks
{
    public class PaymentService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPaymentCheckRepository _paymentRepository;
        private readonly IUserRepository _userRepository;

        public PaymentService(IPaymentCheckRepository paymentRepository, IUserRepository userRepository, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<PaymentCheckModel>?> GetPaymentChecksForUserAsync(string userId)
        {
            var payments = await _paymentRepository.GetPaymentChecksForUserAsync(userId);

            return PaymentCheckMapper.GetListPaymentCheckModel(payments);
        }

        public async Task<PaymentCheckModel?> GetPaymentCheckByIdAsync(string id)
        {
            var payment = await _paymentRepository.GetPaymentCheckByIdAsync(id);

            if (payment == null)
                throw new NullReferenceException($"The payment with id:{id} does not exist");

            return PaymentCheckMapper.GetPaymentCheckModel(payment);
        }

        public async Task AddPaymentCheckAsync(PaymentCheckModel paymentCheckModel)
        {
            var payment = PaymentCheckMapper.GetPaymentCheck(paymentCheckModel);

            if (string.IsNullOrEmpty(paymentCheckModel.User?.Id.ToString()))
                throw new ArgumentNullException("The user id is incorrect");

            var user = await _userRepository.GetUserByIdAsync(paymentCheckModel.User.Id.ToString());

            if (user is null)
                throw new NullReferenceException("The user not found");

            payment.User = user;

            var userTags = await _tagRepository.GetTagsForUserAsync(paymentCheckModel.User.Id);

            payment.Products = payment.Products?.Select(product =>
            {
                product.Tags = product.Tags?.Select(tag =>
                {
                    var existingTag = userTags.FirstOrDefault(t => t.Name == tag.Name);

                    tag.User = user;

                    return existingTag == null ? tag : existingTag;
                }).ToList();

                return product;
            }).ToList();

            if (string.IsNullOrWhiteSpace(payment.Name))
                payment.Name = $"Payment ${await _paymentRepository.GetTotalNumberOfPaymentsForUserAsync(paymentCheckModel.User.Id)}";

            await _paymentRepository.AddPaymentCheckAsync(payment);
        }

        public async Task UpdatePaymentCheckAsync(PaymentCheckModel paymentCheckModel)
        {
            var payment = await _paymentRepository.GetPaymentCheckByIdAsync(paymentCheckModel.Id);

            if (payment is null)
                throw new NullReferenceException("The payment not found");

            var userTags = await _tagRepository.GetTagsForUserAsync(paymentCheckModel.User.Id);

            payment.Name = paymentCheckModel.Name;
            payment.Products = ProductMapper.GetListProduct(paymentCheckModel.Products)?
                .Select(product =>
                    {
                        product.Tags = product.Tags?.Select(tag =>
                        {
                            var existingTag = userTags.FirstOrDefault(t => t.Name == tag.Name);

                            tag.User = payment.User;

                            return existingTag == null ? tag : existingTag;
                        }).ToList();

                        return product;
                    }).ToList();

            await _paymentRepository.UpdatePaymentCheckAsync(payment);
        }
    }
}
