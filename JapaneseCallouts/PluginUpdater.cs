namespace JapaneseCallouts;

internal static class PluginUpdater
{
    private const string DATA_JSON = @"https://raw.githubusercontent.com/DekoKiyo/JapaneseCallouts/main/update.json";
    private const string DOWNLOAD_LINK = @"https://github.com/DekoKiyo/JapaneseCallouts/releases/download/{0}/JapaneseCallouts.dll";

    private static readonly HttpClient Client = new();

    internal static bool CheckUpdate()
    {
        var data = Client.GetStringAsync(DATA_JSON).Result;
        var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

        return Version.Parse(dic["latest"]) > Version.Parse(Main.PLUGIN_VERSION);
    }
}