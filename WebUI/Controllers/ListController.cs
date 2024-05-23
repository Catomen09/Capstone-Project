using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebUI.Dtos.CompaniesScoresDtos;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class ListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7114/api/CompaniesScores");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCompaniesScores>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
