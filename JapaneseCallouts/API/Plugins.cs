namespace JapaneseCallouts.API;

internal static class Plugins
{
    internal static bool IsCalloutInterfaceAPIExist()
    {
        return File.Exists(Main.CALLOUT_INTERFACE_API_DLL);
    }
}