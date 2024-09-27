using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLPNumberConverter.CoreLayer.Entities;
using System.Text;

namespace NLPNumberConverter.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly HttpClient _httpClient;

        public DefaultController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Convert(UserText userText)
        {
            var json = JsonConvert.SerializeObject(userText);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7088/api/TextConverter/convert", content);
            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                var convertedText = JsonConvert.DeserializeObject<ConvertedText>(resultJson);
                ViewBag.ConvertedText = convertedText.OutputText;
            }
            else
            {
                ViewBag.ConvertedText = "Dönüşüm başarısız oldu.";
            }

            return View("Index");
        }
    }
}
