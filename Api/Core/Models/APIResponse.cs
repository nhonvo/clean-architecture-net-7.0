using System.Net;

namespace Api.Core.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool Succeeded { get; set; }

        public string? Message { get; set; }

        public object? ResultObject { get; set; }
    }
}