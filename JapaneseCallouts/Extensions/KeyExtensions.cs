namespace JapaneseCallouts.Extensions;

internal static class KeyExtensions
{
    internal static bool IsKeysDown(Keys Main, Keys Modifier = Keys.None)
    => NT.MISC.UPDATE_ONSCREEN_KEYBOARD() is not 0 && (Game.IsKeyDown(Modifier) || Modifier is Keys.None) && Game.IsKeyDown(Main);
    internal static bool IsKeysDownRightNow(Keys Main, Keys Modifier = Keys.None)
    => NT.MISC.UPDATE_ONSCREEN_KEYBOARD() is not 0 && (Game.IsKeyDownRightNow(Modifier) || Modifier is Keys.None) && Game.IsKeyDownRightNow(Main);
    internal static Keys ConvertToKey(this string KeyString) => (Keys)new KeysConverter().ConvertFromString(KeyString);
}