using FinAccountingApi.Application.Accounts;
using FinAccountingApi.Application.Accounts.Auth;
using FinAccountingApi.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinAccountingApi.Presentation.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsService _accountsService;
        public AccountsController(AccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Route("Login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var token = await _accountsService.LoginUserAsync(model);

                return Ok(token);
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegisterModel registerModel)
        {
            await _accountsService.RegisterUser(registerModel);

            return Ok();
        }
    }
}
