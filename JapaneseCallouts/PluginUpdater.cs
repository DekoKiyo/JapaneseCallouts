namespace JapaneseCallouts;

internal static class PluginUpdater
{
    private const string DOWNLOAD_LINK = @"https://github.com/DekoKiyo/JapaneseCallouts/releases/download/{0}/JapaneseCallouts.dll";
    private const string PATH = $"{Main.LSPDFR_DIRECTORY}/{Main.PLUGIN_NAME_NO_SPACE}";
    private const string OLD_FILE_EXTENSION = ".old";
    private const string DLL_FILE_EXTENSION = ".dll";

    internal static bool CheckUpdate()
    {
        if (File.Exists($"{PATH}{OLD_FILE_EXTENSION}"))
        {
            File.Delete($"{PATH}{OLD_FILE_EXTENSION}");
            HudHelpers.DisplayNotification(Localization.GetString("PluginUpdated", Main.PLUGIN_NAME), Main.PLUGIN_NAME, Main.PLUGIN_VERSION_DATA);
        }

        return Version.Parse(Main.RemoteLatestVersion) > Version.Parse(Main.PLUGIN_VERSION);
    }

    internal static void Update()
    {
        try
        {
            var client = new WebClient();
            var uri = new Uri(string.Format(DOWNLOAD_LINK, Main.RemoteLatestVersion));
            if (File.Exists($"{PATH}{DLL_FILE_EXTENSION}")) File.Move($"{PATH}{DLL_FILE_EXTENSION}", $"{PATH}{OLD_FILE_EXTENSION}");
            client.DownloadFile(uri, $"{PATH}{DLL_FILE_EXTENSION}");

            HudHelpers.DisplayNotification(Localization.GetString("UpdateAuto"), Main.PLUGIN_NAME, "");
            Logger.Info($"{Main.PLUGIN_NAME} was updated to {Main.RemoteLatestVersion}.");
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }
    }
}