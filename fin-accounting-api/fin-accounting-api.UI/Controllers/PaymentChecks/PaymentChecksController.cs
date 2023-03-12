using FinAccountingApi.Application.PaymentChecks;
using FinAccountingApi.Application.PaymentChecks.Products;
using FinAccountingApi.Application.PaymentChecks.Products.Tags;
using FinAccountingApi.Presentation.Controllers.PaymentChecks.Models;
using FinAccountingApi.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinAccountingApi.Presentation.Controllers.PaymentChecks
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentChecksController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentChecksController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetPaymentChecksForUser(string userId)
        {
            try
            {
                return Ok(await _paymentService.GetPaymentChecksForUserAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPaymentCheckById(string id)
        {
            try
            {
                return Ok(await _paymentService.GetPaymentCheckByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("AddCheck")]
        public async Task<IActionResult> AddPayment(PaymentCheckFormModel payment)
        {
            var paymentModel = new PaymentCheckModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = payment.Name,
                Date = DateTime.UtcNow,
                Products = payment.Products?.Select(product => new ProductModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    Tags = product.Tags?.Select(tag => new TagModel
                    {
                        Name = tag,
                    })
                }),
                User = new UserModel { Id = payment.UserId }
            };

            try
            {
                await _paymentService.AddPaymentCheckAsync(paymentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Route("UpdateCheck")]
        public async Task<IActionResult> UpdatePayment(PaymentCheckFormModel payment)
        {
            var paymentModel = new PaymentCheckModel
            {
                Id = payment.Id,
                Name = payment.Name,
                Date = payment.Date ?? DateTime.UtcNow,
                Products = payment.Products?.Select(product => new ProductModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    Tags = product.Tags?.Select(tag => new TagModel
                    {
                        Name = tag,
                    })
                }),
                User = new UserModel { Id = payment.UserId }
            };

            try
            {
                await _paymentService.UpdatePaymentCheckAsync(paymentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
