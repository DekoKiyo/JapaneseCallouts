using System.Data;

namespace JapaneseCallouts;

internal static class Settings
{
    internal static Keys SpeakWithThePersonKey { get; } = Keys.Y;
    internal static Keys SpeakWithThePersonModifierKey { get; } = Keys.None;

    internal static InitializationFile SettingFile = LoadFile($"{Main.LSPDFR_DIRECTORY}/{Main.SETTINGS_INI_FILE}");
    private static InitializationFile LoadFile(string path)
    {
        var ini = new InitializationFile(path);
        ini.Create();
        return ini;
    }

    static Settings()
    {
        SpeakWithThePersonKey = SettingFile.Read("Keys", "SpeakWithThePersonKey", Keys.Y);
        SpeakWithThePersonModifierKey = SettingFile.Read("Keys", "SpeakWithThePersonModifierKey", Keys.None);
    }
}