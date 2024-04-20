namespace JapaneseCallouts;

internal static class Settings
{
    private static FileIniDataParser Parser { get; } = new();
    private static IniData Ini { get; } = Parser.ReadFile($"{Main.LSPDFR_DIRECTORY}/{Main.SETTINGS_INI_FILE}");

    internal static string OfficerName { get; private set; } = "Officer";

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
        ReloadJPCSettings();
    }

    [ConsoleCommand("Reload Japanese Callouts' settings.")]
    internal static void ReloadJPCSettings()
    {
        OfficerName = Ini["General"][nameof(OfficerName)] ??= "Officer";
        Localization.Language = Ini["General"][nameof(Localization.Language)] ??= "en-US";
        SpeakWithThePersonKey = (Ini["Keys"][nameof(SpeakWithThePersonKey)] ??= "Y").ConvertToKey();
        SpeakWithThePersonModifierKey = (Ini["Keys"][nameof(SpeakWithThePersonModifierKey)] ??= "None").ConvertToKey();
        ToggleBankHeistAlarmSoundKey = (Ini["Keys"][nameof(ToggleBankHeistAlarmSoundKey)] ??= "F5").ConvertToKey();
        ToggleBankHeistAlarmSoundModifierKey = (Ini["Keys"][nameof(ToggleBankHeistAlarmSoundModifierKey)] ??= "None").ConvertToKey();
        SWATFollowKey = (Ini["Keys"][nameof(SWATFollowKey)] ??= "I").ConvertToKey();
        SWATFollowModifierKey = (Ini["Keys"][nameof(SWATFollowModifierKey)] ??= "None").ConvertToKey();
        HostageRescueKey = (Ini["Keys"][nameof(HostageRescueKey)] ??= "D0").ConvertToKey();
        HostageRescueModifierKey = (Ini["Keys"][nameof(HostageRescueModifierKey)] ??= "None").ConvertToKey();
        EnterRiotVanKey = (Ini["Keys"][nameof(EnterRiotVanKey)] ??= "Enter").ConvertToKey();
        EnterRiotVanModifierKey = (Ini["Keys"][nameof(EnterRiotVanModifierKey)] ??= "None").ConvertToKey();
    }
}