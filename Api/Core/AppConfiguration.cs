#nullable disable

namespace Api.Core
{
    public class AppConfiguration
    {
        public bool UseInMemoryDatabase { get; set; }
        public bool UseRedisCache { get; set; }
        public bool UseDocker { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public AzureTranslate AzureTranslate { get; set; }
        public Jwt Jwt { get; set; }
    }
    public class AzureTranslate
    {
        public string Key { get; set; }
        public string Endpoint { get; set; }
        public string Location { get; set; }
        public string DefaultLanguage { get; set; }
        public string TranslateLanguage { get; set; }
        public string Route { get; set; }
    }
    public class ConnectionStrings
    {
        public string DatabaseConnection { get; set; }
        public string DatabaseConnectionDocker { get; set; }
        public string RedisConnectionDocker { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

}