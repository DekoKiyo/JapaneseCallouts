namespace JapaneseCallouts;

internal class Settings
{
    private static FileIniDataParser Parser { get; } = new();
    private static IniData Ini { get; } = Parser.ReadFile($"{Main.LSPDFR_DIRECTORY}/{Main.SETTINGS_INI_FILE}", Encoding.UTF8);

    internal static Settings Instance { get; } = new();

    internal string OfficerName { get; private set; } = "Officer";
    internal bool EnableAutoUpdate { get; private set; } = false;

    internal Keys EndCalloutsKey { get; private set; } = Keys.End;
    internal Keys EndCalloutsModifierKey { get; private set; } = Keys.None;
    internal Keys SpeakWithThePersonKey { get; private set; } = Keys.Y;
    internal Keys SpeakWithThePersonModifierKey { get; private set; } = Keys.None;
    internal Keys ToggleBankHeistAlarmSoundKey { get; private set; } = Keys.F5;
    internal Keys ToggleBankHeistAlarmSoundModifierKey { get; private set; } = Keys.None;
    internal Keys SWATFollowKey { get; private set; } = Keys.I;
    internal Keys SWATFollowModifierKey { get; private set; } = Keys.None;
    internal Keys HostageRescueKey { get; private set; } = Keys.D0;
    internal Keys HostageRescueModifierKey { get; private set; } = Keys.None;
    internal Keys EnterRiotVanKey { get; private set; } = Keys.Enter;
    internal Keys EnterRiotVanModifierKey { get; private set; } = Keys.None;

    internal string BankHeistRadioSound { get; private set; } = "WE_HAVE JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99";
    internal string PacificBankHeistRadioSound { get; private set; } = "CITIZENS_REPORT JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99";
    internal string DrunkGuysRadioSound { get; private set; } = "CITIZENS_REPORT JP_CRIME_ACCIDENT IN_OR_ON_POSITION";
    internal string RoadRageRadioSound { get; private set; } = "CITIZENS_REPORT JP_CRIME_TRAFFIC_VIOLATION IN_OR_ON_POSITION";
    internal string StolenVehicleRadioSound { get; private set; } = "WE_HAVE JP_CRIME_STOLEN_VEHICLE IN_OR_ON_POSITION";
    internal string StoreRobberyRadioSound { get; private set; } = "CITIZENS_REPORT CRIME_ROBBERY IN_OR_ON_POSITION";
    internal string HotPursuitRadioSound { get; private set; } = "WE_HAVE CRIME_GRAND_THEFT_AUTO IN_OR_ON_POSITION";
    internal string WantedCriminalFoundRadioSound { get; private set; } = "ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_SUSPECT_RESISTING_ARREST IN_OR_ON_POSITION";
    internal string StreetFightRadioSound { get; private set; } = "ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_CIVIL_DISTURBANCE IN_OR_ON_POSITION";

    internal bool EnableBankHeist { get; private set; } = true;
    internal bool EnablePacificBankHeist { get; private set; } = true;
    internal bool EnableDrunkGuys { get; private set; } = true;
    internal bool EnableRoadRage { get; private set; } = true;
    internal bool EnableStolenVehicle { get; private set; } = true;
    internal bool EnableStoreRobbery { get; private set; } = true;
    internal bool EnableHotPursuit { get; private set; } = true;
    internal bool EnableWantedCriminalFound { get; private set; } = true;
    internal bool EnableStreetFight { get; private set; } = true;

    internal Settings()
    {
        Init();
    }

    [ConsoleCommand(Name = "JPC_ReloadSettings", Description = "Reload Japanese Callouts' Settings.Instance.")]
    private void Init()
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

        EnableBankHeist = bool.Parse(Ini["Callouts"][nameof(EnableBankHeist)] ??= "true");
        EnablePacificBankHeist = bool.Parse(Ini["Callouts"][nameof(EnablePacificBankHeist)] ??= "true");
        EnableDrunkGuys = bool.Parse(Ini["Callouts"][nameof(EnableDrunkGuys)] ??= "true");
        EnableRoadRage = bool.Parse(Ini["Callouts"][nameof(EnableRoadRage)] ??= "true");
        EnableStolenVehicle = bool.Parse(Ini["Callouts"][nameof(EnableStolenVehicle)] ??= "true");
        EnableStoreRobbery = bool.Parse(Ini["Callouts"][nameof(EnableStoreRobbery)] ??= "true");
        EnableHotPursuit = bool.Parse(Ini["Callouts"][nameof(EnableHotPursuit)] ??= "true");
        EnableWantedCriminalFound = bool.Parse(Ini["Callouts"][nameof(EnableWantedCriminalFound)] ??= "true");
        EnableStreetFight = bool.Parse(Ini["Callouts"][nameof(EnableStreetFight)] ??= "true");

        // Log
        Main.Logger.Info("=================== Japanese Callouts Settings ===================");
        Main.Logger.Info("General Settings", "Settings");
        Main.Logger.Info($"{nameof(OfficerName)}: {OfficerName.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(Localization.Language)}: {Localization.Language.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(EnableAutoUpdate)}: {EnableAutoUpdate.ToString()}", "Settings");
        Main.Logger.Info("\nKeys Settings", "Settings");
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
        Main.Logger.Info("\nSounds Settings", "Settings");
        Main.Logger.Info($"{nameof(BankHeistRadioSound)}: {BankHeistRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(PacificBankHeistRadioSound)}: {PacificBankHeistRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(DrunkGuysRadioSound)}: {DrunkGuysRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(RoadRageRadioSound)}: {RoadRageRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(StolenVehicleRadioSound)}: {StolenVehicleRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(StoreRobberyRadioSound)}: {StoreRobberyRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(HotPursuitRadioSound)}: {HotPursuitRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(WantedCriminalFoundRadioSound)}: {WantedCriminalFoundRadioSound.ToString()}", "Settings");
        Main.Logger.Info($"{nameof(StreetFightRadioSound)}: {StreetFightRadioSound.ToString()}", "Settings");
        Main.Logger.Info("\nCallouts Settings");
        Main.Logger.Info($"{nameof(EnableBankHeist)}: {EnableBankHeist.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnablePacificBankHeist)}: {EnablePacificBankHeist.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableDrunkGuys)}: {EnableDrunkGuys.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableRoadRage)}: {EnableRoadRage.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableStolenVehicle)}: {EnableStolenVehicle.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableStoreRobbery)}: {EnableStoreRobbery.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableHotPursuit)}: {EnableHotPursuit.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableWantedCriminalFound)}: {EnableWantedCriminalFound.ToString().ToLower()}", "Settings");
        Main.Logger.Info($"{nameof(EnableStreetFight)}: {EnableStreetFight.ToString().ToLower()}", "Settings");
        Main.Logger.Info("=================== Japanese Callouts Settings ===================");
    }
}