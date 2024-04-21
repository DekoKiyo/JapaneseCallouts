namespace JapaneseCallouts;

internal static class PluginUpdater
{
    private const string DATA_JSON = @"https://raw.githubusercontent.com/DekoKiyo/JapaneseCallouts/main/update.json";
    private const string DOWNLOAD_LINK = @"https://github.com/DekoKiyo/JapaneseCallouts/releases/download/{0}/JapaneseCallouts.dll";
    internal static string LatestVersion = Main.PLUGIN_VERSION;
    private const string DLL_PATH = $"{Main.LSPDFR_DIRECTORY}/{Main.PLUGIN_NAME_NO_SPACE}.dll";
    private const string OLD_PATH = $"{Main.LSPDFR_DIRECTORY}/{Main.PLUGIN_NAME_NO_SPACE}.old";

    private static readonly HttpClient Client = new();

    internal static bool CheckUpdate()
    {
        if (File.Exists(OLD_PATH))
        {
            File.Delete(OLD_PATH);
            HudHelpers.DisplayNotification(Localization.GetString("PluginUpdated", Main.PLUGIN_NAME), Main.PLUGIN_NAME, Main.PLUGIN_VERSION_DATA);
        }

        var data = Client.GetStringAsync(DATA_JSON).Result;
        var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
        LatestVersion = dic["latest"];

        return Version.Parse(LatestVersion) > Version.Parse(Main.PLUGIN_VERSION);
    }

    internal static async void Update()
    {
        var data = await Client.GetByteArrayAsync(string.Format(DOWNLOAD_LINK, LatestVersion));

        if (File.Exists(OLD_PATH)) File.Delete(OLD_PATH);
        if (File.Exists(DLL_PATH)) File.Move(DLL_PATH, OLD_PATH);

        File.WriteAllBytes(DLL_PATH, data);

        HudHelpers.DisplayNotification(Localization.GetString("UpdateAuto"), Main.PLUGIN_NAME, "");
        Logger.Info($"{Main.PLUGIN_NAME} was updated to {LatestVersion}.");
    }
}