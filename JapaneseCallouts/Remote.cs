namespace JapaneseCallouts;

internal static class Remote
{
    private const string DATA_JSON = @"https://raw.githubusercontent.com/DekoKiyo/JapaneseCallouts/main/plugin_data.json";

    private static HttpClient HttpClient { get; } = new();

    internal static async void Initialize()
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            var data = await HttpClient.GetStringAsync(DATA_JSON);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            Main.RemoteLatestVersion = (string)json["latest"];
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }
    }

    internal static async Task<string> GetStringAsync(string uri)
    {
        return await HttpClient.GetStringAsync(uri);
    }

    internal static async Task<byte[]> GetByteArrayAsync(string uri)
    {
        return await HttpClient.GetByteArrayAsync(uri);
    }
}