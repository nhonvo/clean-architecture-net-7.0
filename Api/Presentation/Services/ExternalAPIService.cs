using System.Net.Http.Headers;
using System.Text;
using Api.Core.Models;
using Api.Core.Utilities;
using Newtonsoft.Json;

namespace Api.Presentation.Services
{
    public class ExternalAPIService
    {
        private readonly IHttpClientFactory _httpClient;

        public ExternalAPIService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest, string? token)
        {
            try
            {
                var client = _httpClient.CreateClient("name_client");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }
                HttpResponseMessage responseMessage =  await client.SendAsync(message);
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(responseContent);
                return APIResponse ?? throw new ArgumentNullException("APIresponse nothing");
            }
            catch (Exception exception)
            {
                var response = new APIResponse
                {
                    Message = exception.ToString(),
                    Succeeded = false,
                };

                var res = JsonConvert.SerializeObject(response);
                var APIresponse = JsonConvert.DeserializeObject<T>(res);
                return APIresponse ?? throw new ArgumentNullException("APIresponse nothing");
            }
        }
    }
}