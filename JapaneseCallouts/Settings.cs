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
        Logger.Info($"{nameof(OfficerName)}: {OfficerName.ToString()}", "Settings");
        Logger.Info($"{nameof(Localization.Language)}: {Localization.Language.ToString()}", "Settings");
        Logger.Info($"{nameof(EnableAutoUpdate)}: {EnableAutoUpdate.ToString()}", "Settings");
        Logger.Info("Keys Settings", "Settings");
        Logger.Info($"{nameof(EndCalloutsKey)}: {EndCalloutsKey.ToString()}", "Settings");
        Logger.Info($"{nameof(EndCalloutsModifierKey)}: {EndCalloutsModifierKey.ToString()}", "Settings");
        Logger.Info($"{nameof(SpeakWithThePersonKey)}: {SpeakWithThePersonKey.ToString()}", "Settings");
        Logger.Info($"{nameof(SpeakWithThePersonModifierKey)}: {SpeakWithThePersonModifierKey.ToString()}", "Settings");
        Logger.Info($"{nameof(ToggleBankHeistAlarmSoundKey)}: {ToggleBankHeistAlarmSoundKey.ToString()}", "Settings");
        Logger.Info($"{nameof(ToggleBankHeistAlarmSoundModifierKey)}: {ToggleBankHeistAlarmSoundModifierKey.ToString()}", "Settings");
        Logger.Info($"{nameof(SWATFollowKey)}: {SWATFollowKey.ToString()}", "Settings");
        Logger.Info($"{nameof(SWATFollowModifierKey)}: {SWATFollowModifierKey.ToString()}", "Settings");
        Logger.Info($"{nameof(HostageRescueKey)}: {HostageRescueKey.ToString()}", "Settings");
        Logger.Info($"{nameof(HostageRescueModifierKey)}: {HostageRescueModifierKey.ToString()}", "Settings");
        Logger.Info($"{nameof(EnterRiotVanKey)}: {EnterRiotVanKey.ToString()}", "Settings");
        Logger.Info($"{nameof(EnterRiotVanModifierKey)}: {EnterRiotVanModifierKey.ToString()}", "Settings");
        Logger.Info("=================== Japanese Callouts Settings ===================");
    }
}