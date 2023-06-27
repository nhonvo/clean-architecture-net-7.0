using System.Text.Json.Serialization;

namespace Api.Core.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Subscriber
    }
}