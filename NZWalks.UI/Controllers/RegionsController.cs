using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = [];
            try
            {

                // Get All regions from Web API
                var client = httpClientFactory.CreateClient();
                var htttpResponseMessage = await client.GetAsync("https://localhost:7182/api/v1/Regions");
                htttpResponseMessage.EnsureSuccessStatusCode();
                var regions = await htttpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();
                if (regions != null)
                {
                    response.AddRange(regions);
                }
            }
            catch (Exception ex)
            {
                // Log the exeption
                throw;
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7182/api/v1/Regions"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"),
                };

                var htttpResponseMessage = await client.SendAsync(httpRequestMessage);

                htttpResponseMessage.EnsureSuccessStatusCode();

                var response = await htttpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception ex)
            {
                // Log the exeption
                throw;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7182/api/v1/Regions/{id}");

            if (response != null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7182/api/v1/Regions/{request.Id}"),
                    Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"),
                };

                var htttpResponseMessage = await client.SendAsync(httpRequestMessage);

                htttpResponseMessage.EnsureSuccessStatusCode();

                var response = await htttpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Regions");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Log the exeption
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var htttpResponseMessage = await client.DeleteAsync($"https://localhost:7182/api/v1/Regions/{request.Id}");

                htttpResponseMessage.EnsureSuccessStatusCode();

                var response = await htttpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Regions");
                }
                return View();
            }
            catch (Exception ex)
            {
                // Log the exeption
                throw;
            }
        }
    }
}
