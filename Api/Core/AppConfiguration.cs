#nullable disable

namespace Api.Core
{
    public class AppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Jwt Jwt { get; set; }
        public Options Options { get; set; }
    }

    public class Options
    {
        public bool Redis { get; set; }
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