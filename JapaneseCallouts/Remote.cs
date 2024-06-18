namespace JapaneseCallouts;

internal static class Remote
{
    private const string DATA_JSON = @"https://raw.githubusercontent.com/DekoKiyo/JapaneseCallouts/main/plugin_data.json";

    internal static void Initialize()
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

            var client = new WebClient();
            var uri = new Uri(DATA_JSON);
            var data = client.DownloadString(uri).Trim();
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            Main.RemoteLatestVersion = json["latest"];
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }
    }
}