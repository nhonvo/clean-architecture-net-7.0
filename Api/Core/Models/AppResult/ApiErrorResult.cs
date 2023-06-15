using System.Net;

namespace Api.Core.Models.AppResult
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public List<string> ValidationErrors { get; set; } = new List<string>();

        public ApiErrorResult()
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
        }

        public ApiErrorResult(string message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
            Message = message;
            ValidationErrors.Add(message);
        }

        public ApiErrorResult(List<string> validationErrors)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
            ValidationErrors = validationErrors;
        }
        public ApiErrorResult(string message, List<string> validationErrors)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
            Message = message;
            ValidationErrors = validationErrors;
        }
    }
}