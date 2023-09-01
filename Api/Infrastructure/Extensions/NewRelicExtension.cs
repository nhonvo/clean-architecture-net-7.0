namespace Api.Infrastructure.Extensions
{
    public static class NewRelicExtension
    {
        public static void CustomMonitor(string name, List<string> messages)
        {
            var attributes = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Name", name)
            };

            for (int i = 0; i < messages.Count; i++)
            {
                var attributeName = $"Message{i + 1}";
                attributes.Add(new KeyValuePair<string, object>(attributeName, messages[i]));
            }
            NewRelic.Api.Agent.NewRelic.RecordCustomEvent("NewRelicMonitor", attributes);
        }
        public static void ErrorCustomMonitor(string name, string message)
        {
            var attributes = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Error Name", name),
                new KeyValuePair<string, object>("Error Message", message),
                new KeyValuePair<string, object>("Type", "Schedule"),
                new KeyValuePair<string, object>("SubType", true)
            };
            NewRelic.Api.Agent.NewRelic.RecordCustomEvent("NewRelicErrorMonitor", attributes);
        }
        public static void BookErrorMonitor(string name, string message)
        {
            var attributes = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Error Name", name),
                new KeyValuePair<string, object>("Error Book Message", message),
                new KeyValuePair<string, object>("Type", "Not Schedule"),
                new KeyValuePair<string, object>("SubType", false)
            };
            NewRelic.Api.Agent.NewRelic.RecordCustomEvent("NewRelicBookErrorMonitor", attributes);
        }
    }
}