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

    internal static string BankHeistRadioSound { get; set; } = "WE_HAVE JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99";
    internal static string PacificBankHeistRadioSound { get; set; } = "CITIZENS_REPORT JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99";
    internal static string DrunkGuysRadioSound { get; set; } = "CITIZENS_REPORT JP_CRIME_ACCIDENT IN_OR_ON_POSITION";
    internal static string RoadRageRadioSound { get; set; } = "CITIZENS_REPORT JP_CRIME_TRAFFIC_VIOLATION IN_OR_ON_POSITION";
    internal static string StolenVehicleRadioSound { get; set; } = "WE_HAVE JP_CRIME_STOLEN_VEHICLE IN_OR_ON_POSITION";
    internal static string StoreRobberyRadioSound { get; set; } = "CITIZENS_REPORT CRIME_ROBBERY IN_OR_ON_POSITION";
    internal static string HotPursuitRadioSound { get; set; } = "WE_HAVE CRIME_GRAND_THEFT_AUTO IN_OR_ON_POSITION";
    internal static string WantedCriminalFoundRadioSound { get; set; } = "ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_SUSPECT_RESISTING_ARREST IN_OR_ON_POSITION";
    internal static string StreetFightRadioSound { get; set; } = "ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_CIVIL_DISTURBANCE IN_OR_ON_POSITION";

    [ConsoleCommand(Name = "JPC_ReloadSettings", Description = "Reload Japanese Callouts' settings.")]
    internal static void Initialize()
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

        BankHeistRadioSound = Ini["Sounds"][nameof(BankHeistRadioSound)] ??= "WE_HAVE JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99";
        PacificBankHeistRadioSound = Ini["Sounds"][nameof(PacificBankHeistRadioSound)] ??= "CITIZENS_REPORT JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99";
        DrunkGuysRadioSound = Ini["Sounds"][nameof(DrunkGuysRadioSound)] ??= "CITIZENS_REPORT JP_CRIME_ACCIDENT IN_OR_ON_POSITION";
        RoadRageRadioSound = Ini["Sounds"][nameof(RoadRageRadioSound)] ??= "CITIZENS_REPORT JP_CRIME_TRAFFIC_VIOLATION IN_OR_ON_POSITION";
        StolenVehicleRadioSound = Ini["Sounds"][nameof(StolenVehicleRadioSound)] ??= "WE_HAVE JP_CRIME_STOLEN_VEHICLE IN_OR_ON_POSITION";
        StoreRobberyRadioSound = Ini["Sounds"][nameof(StoreRobberyRadioSound)] ??= "CITIZENS_REPORT CRIME_ROBBERY IN_OR_ON_POSITION";
        HotPursuitRadioSound = Ini["Sounds"][nameof(HotPursuitRadioSound)] ??= "WE_HAVE CRIME_GRAND_THEFT_AUTO IN_OR_ON_POSITION";
        WantedCriminalFoundRadioSound = Ini["Sounds"][nameof(WantedCriminalFoundRadioSound)] ??= "ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_SUSPECT_RESISTING_ARREST IN_OR_ON_POSITION";
        StreetFightRadioSound = Ini["Sounds"][nameof(StreetFightRadioSound)] ??= "ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_CIVIL_DISTURBANCE IN_OR_ON_POSITION";

        Main.Logger.Info("=================== Japanese Callouts Settings ===================");
        Main.Logger.Info("General Settings", "Settings");
        Main.Logger.Info($"{nameof(OfficerName)}: {OfficerName.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(Localization.Language)}: {Localization.Language.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(EnableAutoUpdate)}: {EnableAutoUpdate.ToString()}", "Settings");
        Main.Logger.Info(string.Empty);
        Main.Logger.Info("Keys Settings", "Settings");
        Main.Logger.Info($"{nameof(EndCalloutsKey)}: {EndCalloutsKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(EndCalloutsModifierKey)}: {EndCalloutsModifierKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(SpeakWithThePersonKey)}: {SpeakWithThePersonKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(SpeakWithThePersonModifierKey)}: {SpeakWithThePersonModifierKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(ToggleBankHeistAlarmSoundKey)}: {ToggleBankHeistAlarmSoundKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(ToggleBankHeistAlarmSoundModifierKey)}: {ToggleBankHeistAlarmSoundModifierKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(SWATFollowKey)}: {SWATFollowKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(SWATFollowModifierKey)}: {SWATFollowModifierKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(HostageRescueKey)}: {HostageRescueKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(HostageRescueModifierKey)}: {HostageRescueModifierKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(EnterRiotVanKey)}: {EnterRiotVanKey.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(EnterRiotVanModifierKey)}: {EnterRiotVanModifierKey.ToString()}", "Settings");
        Main.Logger.Info(string.Empty);
        Main.Logger.Info($"Sounds Settings", "Settings");
        Main.Logger.Info($"{nameof(BankHeistRadioSound)}: {BankHeistRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(PacificBankHeistRadioSound)}: {PacificBankHeistRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(DrunkGuysRadioSound)}: {DrunkGuysRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(RoadRageRadioSound)}: {RoadRageRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(StolenVehicleRadioSound)}: {StolenVehicleRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(StoreRobberyRadioSound)}: {StoreRobberyRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(HotPursuitRadioSound)}: {HotPursuitRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(WantedCriminalFoundRadioSound)}: {WantedCriminalFoundRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(StreetFightRadioSound)}: {StreetFightRadioSound.ToString()}", "Settings");
        Main.Logger.Info("=================== Japanese Callouts Settings ===================");
    }
}