using Api.ApplicationLogic.Interface;

namespace Api.ApplicationLogic.Services
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime() => DateTime.UtcNow;
    }
}