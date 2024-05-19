namespace JapaneseCallouts;

internal static class Settings
{
    private static FileIniDataParser Parser { get; } = new();
    private static IniData Ini { get; } = Parser.ReadFile($"{Main.LSPDFR_DIRECTORY}/{Main.SETTINGS_INI_FILE}", Encoding.UTF8);

    internal static string OfficerName { get; private set; } = "Officer";
    internal static bool EnableAutoUpdate { get; private set; } = false;

    internal static Keys EndCalloutsKey { get; private set; } = Keys.End;
    internal static Keys EndCalloutsModifierKey { get; private set; } = Keys.None;
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
        JPCReloadSettings();
    }

    [ConsoleCommand("Reload Japanese Callouts' settings.")]
    internal static void JPCReloadSettings()
    {
        OfficerName = Ini["General"][nameof(OfficerName)] ??= "Officer";
        Localization.Language = (ELanguages)int.Parse(Ini["General"][nameof(Localization.Language)]);
        EnableAutoUpdate = bool.Parse(Ini["General"][nameof(EnableAutoUpdate)] ??= "false");

        EndCalloutsKey = (Ini["Keys"][nameof(EndCalloutsKey)] ??= "End").ConvertToKey();
        EndCalloutsModifierKey = (Ini["Keys"][nameof(EndCalloutsModifierKey)] ??= "None").ConvertToKey();
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

        Log();
    }

    private static void Log()
    {
        Logger.Info("=================== Japanese Callouts Settings ===================");
        Logger.Info("General Settings", "Settings");
        Logger.Info($"{nameof(OfficerName)}: {OfficerName}", "Settings");
        Logger.Info($"{nameof(Localization.Language)}: {Localization.Language}", "Settings");
        Logger.Info($"{nameof(EnableAutoUpdate)}: {EnableAutoUpdate}", "Settings");
        Logger.Info("Keys Settings", "Settings");
        Logger.Info($"{nameof(EndCalloutsKey)}: {EndCalloutsKey}", "Settings");
        Logger.Info($"{nameof(EndCalloutsModifierKey)}: {EndCalloutsModifierKey}", "Settings");
        Logger.Info($"{nameof(SpeakWithThePersonKey)}: {SpeakWithThePersonKey}", "Settings");
        Logger.Info($"{nameof(SpeakWithThePersonModifierKey)}: {SpeakWithThePersonModifierKey}", "Settings");
        Logger.Info($"{nameof(ToggleBankHeistAlarmSoundKey)}: {ToggleBankHeistAlarmSoundKey}", "Settings");
        Logger.Info($"{nameof(ToggleBankHeistAlarmSoundModifierKey)}: {ToggleBankHeistAlarmSoundModifierKey}", "Settings");
        Logger.Info($"{nameof(SWATFollowKey)}: {SWATFollowKey}", "Settings");
        Logger.Info($"{nameof(SWATFollowModifierKey)}: {SWATFollowModifierKey}", "Settings");
        Logger.Info($"{nameof(HostageRescueKey)}: {HostageRescueKey}", "Settings");
        Logger.Info($"{nameof(HostageRescueModifierKey)}: {HostageRescueModifierKey}", "Settings");
        Logger.Info($"{nameof(EnterRiotVanKey)}: {EnterRiotVanKey}", "Settings");
        Logger.Info($"{nameof(EnterRiotVanModifierKey)}: {EnterRiotVanModifierKey}", "Settings");
        Logger.Info("=================== Japanese Callouts Settings ===================");
    }
}