using FinAccountingApi.Application.PaymentChecks.Products.Tags;
using Microsoft.AspNetCore.Mvc;

namespace FinAccountingApi.Presentation.Controllers.PaymentChecks.Products.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagsController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetTagsForUser(string userId)
        {
            try
            {
                var tags = await _tagService.GetTagsForUser(userId);

                return Ok(tags);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
