using static Models.Utilities.SD;

namespace Models.ApiResult
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; } = string.Empty;

        public object? Data { get; set; }

        public string? Token { get; set; }
    }
}