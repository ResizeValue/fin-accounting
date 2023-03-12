using FinAccountingApi.Application.Resources.Forms;
using FinAccountingApi.Application.Resources.Mapper;
using FinAccountingApi.Application.Resources.OwnershipCost;
using FinAccountingApi.Application.Users;
using FinAccountingApi.Domain.Resources;
using Microsoft.Extensions.Configuration;

namespace FinAccountingApi.Application.Resources
{
    public class ResourceService
    {
        private readonly IConfiguration _configuration;
        private readonly IResourceRepository _resourceRepository;
        private readonly IUserRepository _userRepository;

        public ResourceService(IConfiguration configuration,
            IResourceRepository resourceRepository,
            IUserRepository userRepository)
        {
            _configuration = configuration;
            _resourceRepository = resourceRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ResourceModel>> GetUserResources(string id)
        {
            var resources = await _resourceRepository.GetResourcesForUserAsync(id);

            return ResourceMapper.GetListResourceModel(resources);
        }

        public async Task<ResourceModel> GetResourceById(int id)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(id);

            return ResourceMapper.GetResourceModel(resource);
        }

        public async Task DeleteResource(int id)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(id);

            await _resourceRepository.DeleteResourceAsync(resource);
        }

        public async Task AddResourceToUser(ResourceForm resourceForm, string path)
        {
            var user = await _userRepository.GetUserByIdAsync(resourceForm.UserId);

            var file = resourceForm.Image;

            string filePath = null;

            var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            if (file != null)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var savePath = Path.Combine(root, fileName);

                filePath = Path.Combine(path, "Images", fileName);

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            var resource = new UserResource
            {
                CreationTime = DateTime.Now,
                Cost = resourceForm.Cost,
                Name = resourceForm.Name,
                Image = filePath,
                Owner = user,
                UserResourceId = resourceForm.ResourceId
            };

            await _resourceRepository.AddResourceAsync(resource);
        }

        public async Task UpdateResource(ResourceForm resourceForm, string path)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(resourceForm.Id.Value);

            var file = resourceForm.Image;

            string filePath = null;

            var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            if (file != null)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                var savePath = Path.Combine(root, fileName);

                filePath = Path.Combine(path, "Images", fileName);

                resource.Image = filePath;

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            resource.Cost = resourceForm.Cost;
            resource.Name = resourceForm.Name;
            resource.Description = resourceForm.Description;

            await _resourceRepository.UpdateResourceAsync(resource);
        }

        public async Task AddOwnershipCostToResource(OwnershipCostForm costForm)
        {
            var resource = await _resourceRepository.GetResourceByIdAsync(costForm.ResourceId);

            var model = new Domain.Resources.OwnershipCost
            {
                Cost = costForm.Cost,
                Name = costForm.Name,
                Periodicity = costForm.Periodicity,
                Description = costForm.Description,
                Resource = resource
            };

            await _resourceRepository.AddOwnershipCostToResourceAsync(model);
        }

        private ICollection<UserResource> ReadImagesToString(ICollection<UserResource> resources)
        {
            foreach (var resource in resources)
            {
                try
                {
                    var image = File.ReadAllBytes(resource.Image);
                    resource.Image = Convert.ToBase64String(image);
                }
                catch { }
            }
            return resources;
        }
    }
}
