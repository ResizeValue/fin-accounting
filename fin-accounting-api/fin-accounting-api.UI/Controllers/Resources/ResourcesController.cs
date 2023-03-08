using FinAccountingApi.Application.Resources;
using FinAccountingApi.Application.Resources.Forms;
using FinAccountingApi.Application.Resources.OwnershipCost;
using FinAccountingApi.Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace FinAccountingApi.UI.Controllers.Resources
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ResourceService _resourceService;
        private readonly IConfiguration _config;
        public ResourcesController(ResourceService resourceService,
            IConfiguration config)
        {
            _resourceService = resourceService;
            _config = config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("addResource")]
        public async Task<IActionResult> AddResource([FromForm]ResourceForm resource)
        {
            var domain = _config.GetSection("Web")["Domain"];

            await _resourceService.AddResourceToUser(resource, domain);

            return Ok();
        }

        [HttpPost]
        [Route("addOwnershipCost")]
        public async Task<IActionResult> AddOwnershipCost([FromForm]OwnershipCostForm ownershipCost)
        {
            await _resourceService.AddOwnershipCostToResource(ownershipCost);

            return Ok();
        }

        [HttpPut]
        [Route("updateUserResource")]
        public async Task<IActionResult> UpdateUserResource([FromForm] ResourceForm resource)
        {
            var domain = _config.GetSection("Web")["Domain"];

            await _resourceService.UpdateResource(resource, domain);

            return Ok();
        }

        [HttpDelete]
        [Route("deleteUserResource/{id}")]
        public async Task<IActionResult> DeleteUserResource(int id)
        {
            await _resourceService.DeleteResource(id);

            return Ok();
        }

        [HttpGet]
        [Route("resourceId/{id}")]
        public async Task<IActionResult> GetUserResource(int id)
        {
            var resource = await _resourceService.GetResourceById(id);

            return Ok(resource);
        }

        [HttpGet]
        [Route("userId/{id}")]
        public async Task<IActionResult> GetUserResourcesRoot(string id)
        {
            var resources = await _resourceService.GetUserResources(id);

            return Ok(resources);
        }
    }
}
