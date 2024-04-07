namespace JapaneseCallouts;

internal static class Settings
{
    private static FileIniDataParser Parser { get; } = new();
    private static IniData Ini { get; } = Parser.ReadFile($"{Main.LSPDFR_DIRECTORY}/{Main.SETTINGS_INI_FILE}");

    internal static string OfficerName { get; private set; } = "Officer";
    internal static string WifeName { get; private set; } = "Maria";

    internal static Keys SpeakWithThePersonKey { get; private set; } = Keys.Y;
    internal static Keys SpeakWithThePersonModifierKey { get; private set; } = Keys.None;
    internal static Keys ToggleBankHeistAlarmSoundKey { get; private set; } = Keys.F5;
    internal static Keys ToggleBankHeistAlarmSoundModifierKey { get; private set; } = Keys.None;
    internal static Keys SWATFollowKey { get; private set; } = Keys.I;
    internal static Keys SWATFollowModifierKey { get; private set; } = Keys.None;
    internal static Keys HostageRescueKey { get; private set; } = Keys.D0;
    internal static Keys HostageRescueModifierKey { get; private set; } = Keys.None;
    internal static Keys EnterRiotVanKey { get; private set; } = Keys.Enter;
    internal static Keys EnterRiotVanModifierKey { get; private set; } = Keys.None;

    internal static void Initialize()
    {
        Load();
    }

    internal static void Load()
    {
        OfficerName = Ini["General"]["OfficerName"] ??= "Officer";
        WifeName = Ini["General"]["WifeName"] ??= "Maria";
        Localization.Language = Ini["General"]["Language"] ??= "en-US";
        SpeakWithThePersonKey = (Ini["Keys"]["SpeakWithThePersonKey"] ??= "Y").ConvertToKey();
        SpeakWithThePersonModifierKey = (Ini["Keys"]["SpeakWithThePersonModifierKey"] ??= "None").ConvertToKey();
        ToggleBankHeistAlarmSoundKey = (Ini["Keys"]["ToggleBankHeistAlarmSoundKey"] ??= "F5").ConvertToKey();
        ToggleBankHeistAlarmSoundModifierKey = (Ini["Keys"]["ToggleBankHeistAlarmSoundModifierKey"] ??= "None").ConvertToKey();
        SWATFollowKey = (Ini["Keys"]["SWATFollowKey"] ??= "I").ConvertToKey();
        SWATFollowModifierKey = (Ini["Keys"]["SWATFollowModifierKey"] ??= "None").ConvertToKey();
        HostageRescueKey = (Ini["Keys"]["HostageRescueKey"] ??= "D0").ConvertToKey();
        HostageRescueModifierKey = (Ini["Keys"]["HostageRescueModifierKey"] ??= "None").ConvertToKey();
        EnterRiotVanKey = (Ini["Keys"]["EnterRiotVanKey"] ??= "Enter").ConvertToKey();
        EnterRiotVanModifierKey = (Ini["Keys"]["EnterRiotVanModifierKey"] ??= "None").ConvertToKey();
    }
}