using System.Text;
using Api.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Presentation.Controller
{
    public class TranslateController : BaseController
    {
        private readonly AppConfiguration _appConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;
        public TranslateController(AppConfiguration appConfiguration, IHttpClientFactory httpClientFactory)
        {
            _appConfiguration = appConfiguration;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> TranslateText(string textToTranslate)
        {
            string route = _appConfiguration.AzureTranslate.Route;
            string key = _appConfiguration.AzureTranslate.Key;
            string location = _appConfiguration.AzureTranslate.Location;
            string endpoint = _appConfiguration.AzureTranslate.Endpoint;
            var client = _httpClientFactory.CreateClient("Azure_Translate");
            
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();

                return Ok(result);
            }
        }
    }
}