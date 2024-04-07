namespace JapaneseCallouts.Helpers;

internal static class KeyHelpers
{
    internal static bool IsKeysDown(Keys Main, Keys Modifier = Keys.None)
    => NativeHelpers.UPDATE_ONSCREEN_KEYBOARD() is not 0 && (Game.IsKeyDown(Modifier) || Modifier is Keys.None) && Game.IsKeyDown(Main);

    internal static bool IsKeysDownRightNow(Keys Main, Keys Modifier = Keys.None)
    => NativeHelpers.UPDATE_ONSCREEN_KEYBOARD() is not 0 && (Game.IsKeyDownRightNow(Modifier) || Modifier is Keys.None) && Game.IsKeyDownRightNow(Main);

    internal static Keys ConvertToKey(this string KeyString) => (Keys)new KeysConverter().ConvertFromString(KeyString);

    internal static void DisplayKeyHelp(string transKey, Keys Main, Keys Modifier = Keys.None, int duration = 5000, bool sound = true)
    {
        var key = Modifier is Keys.None ? $"~{Main.GetInstructionalId()}~" : $"~{Main.GetInstructionalId()}~ ~+~ ~{Modifier.GetInstructionalId()}~";
        HudHelpers.DisplayHelp(Localization.GetString(transKey, key), duration, sound);
    }
    internal static void DisplayKeyHelp(string transKey, string[] args, Keys Main, Keys Modifier = Keys.None, int duration = 5000, bool sound = true)
    {
        var key = Modifier is Keys.None ? $"~{Main.GetInstructionalId()}~" : $"~{Main.GetInstructionalId()}~ ~+~ ~{Modifier.GetInstructionalId()}~";
        HudHelpers.DisplayHelp(Localization.GetString(transKey, [key, .. args]), duration, sound);
    }
}