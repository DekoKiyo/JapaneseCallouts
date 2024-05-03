namespace JapaneseCallouts;

internal static class PluginUpdater
{
    private const string DOWNLOAD_LINK = @"https://github.com/DekoKiyo/JapaneseCallouts/releases/download/{0}/JapaneseCallouts.dll";
    private const string DLL_PATH = $"{Main.LSPDFR_DIRECTORY}/{Main.PLUGIN_NAME_NO_SPACE}.dll";
    private const string OLD_PATH = $"{Main.LSPDFR_DIRECTORY}/{Main.PLUGIN_NAME_NO_SPACE}.old";

    internal static bool CheckUpdate()
    {
        if (File.Exists(OLD_PATH))
        {
            File.Delete(OLD_PATH);
            HudHelpers.DisplayNotification(Localization.GetString("PluginUpdated", Main.PLUGIN_NAME), Main.PLUGIN_NAME, Main.PLUGIN_VERSION_DATA);
        }

        return Version.Parse(Main.RemoteLatestVersion) > Version.Parse(Main.PLUGIN_VERSION);
    }

    internal static async void Update()
    {
        try
        {
            var data = await Remote.GetByteArrayAsync(string.Format(DOWNLOAD_LINK, Main.RemoteLatestVersion));

            if (File.Exists(OLD_PATH)) File.Delete(OLD_PATH);
            if (File.Exists(DLL_PATH)) File.Move(DLL_PATH, OLD_PATH);

            File.WriteAllBytes(DLL_PATH, data);
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }

        HudHelpers.DisplayNotification(Localization.GetString("UpdateAuto"), Main.PLUGIN_NAME, "");
        Logger.Info($"{Main.PLUGIN_NAME} was updated to {Main.RemoteLatestVersion}.");
    }
}