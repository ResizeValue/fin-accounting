using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace fin_accounting_api.Application.Resources.Forms
{
    public class RootResourceForm
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public IFormFile? Image { get; set; }
        public decimal Cost { get; set; }
    }
}
