namespace JapaneseCallouts.Helpers;

internal static class NativeHelpers
{
    internal static void BEGIN_TEXT_COMMAND_PRINT(string GxtEntry)
    => NativeFunction.Natives.BEGIN_TEXT_COMMAND_PRINT(GxtEntry);
    internal static void END_TEXT_COMMAND_PRINT(int duration, bool drawImmediately)
    => NativeFunction.Natives.END_TEXT_COMMAND_PRINT(duration, drawImmediately);
    internal static void BEGIN_TEXT_COMMAND_DISPLAY_HELP(string inputType)
    => NativeFunction.Natives.BEGIN_TEXT_COMMAND_DISPLAY_HELP(inputType);
    internal static void END_TEXT_COMMAND_DISPLAY_HELP(int shape, bool loop, bool beep, int duration)
    => NativeFunction.Natives.END_TEXT_COMMAND_DISPLAY_HELP(shape, loop, beep, duration);
    internal static void BEGIN_TEXT_COMMAND_THEFEED_POST(string text)
    => NativeFunction.Natives.BEGIN_TEXT_COMMAND_THEFEED_POST(text);
    internal static void ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(string text)
    => NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
    internal static void END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(string textureDict, string textureName, bool flash, int iconType, string sender, string subject)
    => NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(textureDict, textureName, flash, iconType, sender, subject);
    internal static void END_TEXT_COMMAND_THEFEED_POST_TICKER(bool isImportant, bool bHasTokens)
    => NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, bHasTokens);
    internal static int UPDATE_ONSCREEN_KEYBOARD()
    => NativeFunction.Natives.UPDATE_ONSCREEN_KEYBOARD<int>();
}