/*
    This callout is based Bank Heist in [Assorted Callouts](https://github.com/Albo1125/Assorted-Callouts) made by [Albo1125](https://github.com/Albo1125).
    The source code is licensed under the GPL-3.0.

    Edited by @DekoKiyo
*/

namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Pacific Bank Heist", CalloutProbability.High)]
internal class PacificBankHeist : CalloutBase
{
    private enum AlarmState
    {
        Alarm,
        None
    }

    private enum NegotiationResult
    {
        Surrender,
        Fight,
    }

    internal const string ALARM_SOUND_FILE_NAME = "BankHeistAlarm.wav";
    private const BlipSprite RIOT_BLIP = BlipSprite.PolicePatrol;
    private const BlipSprite COMMANDER_BLIP = BlipSprite.Friend;

    private SoundPlayer BankAlarm;
    private uint SpeedZoneId;
    private const BlipSprite SPRITE = BlipSprite.CriminalCarsteal;
    private static int HostageCount = XmlManager.PacificBankHeistConfig.HostageCount > XmlManager.PacificBankHeistConfig.HostagePositions.Count ? XmlManager.PacificBankHeistConfig.HostagePositions.Count : XmlManager.PacificBankHeistConfig.HostageCount;

    private Vector3 BankLocation = new(250.9f, 219.0f, 106.2f);
    private Vector3 OutsideBankVault = new(257.2f, 225.2f, 101.8f);
    private readonly Vector3[] PacificBankInsideChecks = [new(235.9f, 220.6f, 106.2f), new(238.3f, 214.8f, 106.2f), new(261.0f, 208.1f, 106.2f), new(235.2f, 217.1f, 106.2f)];
    private readonly Vector3[] BankDoorPositions = [new(231.5f, 215.2f, 106.2f), new(259.1f, 202.7f, 106.2f)];

    // Timer Bars
    internal TimerBarPool TBPool = [];
    internal TextTimerBar RescuedHostagesTB;
    internal TextTimerBar DiedHostagesTB;

    #region Conversations
    // Conversation
    private (string, string)[] IntroConversation;
    private readonly Dictionary<string, Keys> IntroSelection = new()
    {
        [Localization.GetString("IntroSelection1")] = Keys.D1,
        [Localization.GetString("IntroSelection2")] = Keys.D2,
    };
    private readonly (string, string)[] DiscussConversation =
    [
        (Settings.OfficerName, Localization.GetString("Discuss1")),
        (Localization.GetString("Commander"), Localization.GetString("Discuss2")),
        (Settings.OfficerName, Localization.GetString("Discuss3")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm1")),
    ];
    private readonly (string, string)[] FightConversation =
    [
        (Settings.OfficerName, Localization.GetString("Fight1")),
        (Localization.GetString("Commander"), Localization.GetString("Fight2")),
        (Localization.GetString("Commander"), Localization.GetString("Fight3")),
        (Settings.OfficerName, Localization.GetString("Fight4")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm1")),
    ];
    private readonly Dictionary<string, Keys> AlarmSelection = new()
    {
        [Localization.GetString("AlarmSelection1")] = Keys.D1,
        [Localization.GetString("AlarmSelection2")] = Keys.D2,
    };
    private readonly (string, string)[] AlarmOffConversation =
    [
        (Localization.GetString("Commander"), Localization.GetString("Alarm2")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm3")),
    ];
    private readonly (string, string)[] AlarmOnConversation =
    [
        (Localization.GetString("Commander"), Localization.GetString("Alarm4")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm5")),
    ];
    private readonly (string, string)[] NegotiationIntroConversation =
    [
        (Localization.GetString("Robber"), Localization.GetString("Phone1")),
        (Settings.OfficerName, Localization.GetString("Phone2")),
        (Localization.GetString("Robber"), Localization.GetString("Phone3")),
    ];
    private readonly Dictionary<string, Keys> NegotiationIntroSelection = new()
    {
        [Localization.GetString("NegotiationSelection1")] = Keys.D1,
        [Localization.GetString("NegotiationSelection2")] = Keys.D2,
        [Localization.GetString("NegotiationSelection3")] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation1Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation11")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation12")),
    ];
    private readonly Dictionary<string, Keys> Negotiation1Selection = new()
    {
        [Localization.GetString("NegotiationSelection4")] = Keys.D1,
        [Localization.GetString("NegotiationSelection2")] = Keys.D2,
        [Localization.GetString("NegotiationSelection5")] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation11Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation111")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation112")),
    ];
    private readonly Dictionary<string, Keys> Negotiation11Selection = new()
    {
        [Localization.GetString("NegotiationSelection6")] = Keys.D1,
        [Localization.GetString("NegotiationSelection7")] = Keys.D2,
        [Localization.GetString("NegotiationSelection8")] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation13Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation131")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation132")),
    ];
    private readonly Dictionary<string, Keys> Negotiation13Selection = new()
    {
        [Localization.GetString("NegotiationSelection9")] = Keys.D1,
        [Localization.GetString("NegotiationSelection6")] = Keys.D2,
        [Localization.GetString("NegotiationSelection10")] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation131Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1311")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1312")),
    ];
    private readonly (string, string)[] Negotiation133Conversation1 =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1331")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1332")),
    ];
    private readonly (string, string)[] Negotiation133Conversation2 =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1331")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1312")),
    ];
    private readonly (string, string)[] Negotiation111Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1111", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1112")),
    ];
    private readonly (string, string)[] Negotiation112Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1121")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1122")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1123")),
    ];
    private readonly (string, string)[] Negotiation113Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1131")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1132")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1133")),
        (Settings.OfficerName, Localization.GetString("Negotiation1134")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1135")),
    ];
    private readonly (string, string)[] RequestConversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection2")),
        (Localization.GetString("Robber"), Localization.GetString("Request1")),
        (Localization.GetString("Robber"), Localization.GetString("Request2")),
    ];
    private readonly Dictionary<string, Keys> RequestSelection = new()
    {
        [Localization.GetString("RequestSelection1")] = Keys.D1,
        [Localization.GetString("RequestSelection2")] = Keys.D2,
        [Localization.GetString("RequestSelection3")] = Keys.D3,
    };
    private readonly (string, string)[] Request1Conversation =
    [
        (Settings.OfficerName, Localization.GetString("RequestSelection1")),
        (Localization.GetString("Robber"), Localization.GetString("Request11")),
        (Settings.OfficerName, Localization.GetString("Request12")),
        (Localization.GetString("Robber"), Localization.GetString("Request13")),
    ];
    private readonly (string, string)[] Request2Conversation =
    [
        (Settings.OfficerName, Localization.GetString("RequestSelection2")),
        (Localization.GetString("Robber"), Localization.GetString("Request21")),
        (Localization.GetString("Robber"), Localization.GetString("Request22")),
    ];
    private readonly (string, string)[] Request3Conversation =
    [
        (Settings.OfficerName, Localization.GetString("RequestSelection3")),
        (Localization.GetString("Robber"), Localization.GetString("Request31")),
        (Localization.GetString("Robber"), Localization.GetString("Request32")),
        (Localization.GetString("Robber"), Localization.GetString("Request33")),
    ];
    private readonly Dictionary<string, Keys> Request2Selection = new()
    {
        [Localization.GetString("RequestSelection4")] = Keys.D1,
        [Localization.GetString("RequestSelection5")] = Keys.D2,
        [Localization.GetString("RequestSelection6")] = Keys.D3,
    };
    private readonly (string, string)[] Request21Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Request211")),
        (Localization.GetString("Robber"), Localization.GetString("Request212")),
    ];
    private readonly (string, string)[] Request22Conversation1 =
    [
        (Settings.OfficerName, Localization.GetString("Request221", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1112")),
    ];
    private readonly (string, string)[] Request22Conversation2 =
    [
        (Settings.OfficerName, Localization.GetString("Request221", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Request222")),
        (Settings.OfficerName, Localization.GetString("Request223")),
    ];
    private readonly (string, string)[] Request22Conversation3 =
    [
        (XmlManager.PacificBankHeistConfig.WifeName, Localization.GetString("Request224")),
        (XmlManager.PacificBankHeistConfig.WifeName, Localization.GetString("Request225")),
        (Localization.GetString("Robber"), Localization.GetString("Request226", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Request227")),
        (Localization.GetString("Robber"), Localization.GetString("Request228", XmlManager.PacificBankHeistConfig.WifeName)),
    ];
    private readonly (string, string)[] Request22Conversation4 =
    [
        (Settings.OfficerName, Localization.GetString("Request229", XmlManager.PacificBankHeistConfig.WifeName)),
        (XmlManager.PacificBankHeistConfig.WifeName, Localization.GetString("Request220")),
    ];
    private readonly (string, string)[] Negotiation3Conversation =
    [
    (Settings.OfficerName, Localization.GetString("NegotiationSelection3")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation31")),
    ];
    private readonly Dictionary<string, Keys> Negotiation3Selection = new()
    {
        [Localization.GetString("NegotiationSelection1")] = Keys.D1,
        [Localization.GetString("NegotiationSelection11")] = Keys.D2,
        [Localization.GetString("NegotiationSelection12")] = Keys.D3,
        [Localization.GetString("NegotiationSelection10")] = Keys.D4,
    };
    private readonly (string, string)[] Negotiation31Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation11")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation311")),
    ];
    private readonly (string, string)[] Negotiation32Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection11")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation321")),
    ];
    private readonly (string, string)[] Negotiation33Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation331")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation332")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation333")),
    ];
    private readonly (string, string)[] Negotiation34Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection10")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3212")),
    ];
    private readonly Dictionary<string, Keys> Negotiation32Selection = new()
    {
        [Localization.GetString("NegotiationSelection10")] = Keys.D1,
        [Localization.GetString("NegotiationSelection14")] = Keys.D2,
        [Localization.GetString("NegotiationSelection15")] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation321Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation3211")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3212")),
    ];
    private readonly (string, string)[] Negotiation322Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection14")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3221")),
    ];
    private readonly (string, string)[] Negotiation323Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection15")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3231")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3232")),
    ];
    private readonly (string, string)[] AfterSurrendered =
    [
        (Localization.GetString("Commander"), Localization.GetString("Surrender1")),
        (Localization.GetString("Commander"), Localization.GetString("Surrender2")),
        (Localization.GetString("Commander"), Localization.GetString("Surrender3")),
        (Localization.GetString("Commander"), Localization.GetString("Surrender4")),
        (Settings.OfficerName, Localization.GetString("Surrender5")),
    ];
    private readonly (string, string)[] Final =
    [
        (Localization.GetString("Commander"), Localization.GetString("Final1")),
        (Localization.GetString("Commander"), Localization.GetString("Final2")),
        (Settings.OfficerName, Localization.GetString("Final3")),
    ];
    #endregion
    private readonly Model BarrierModel = "PROP_BARRIER_WORK05";
    private readonly Model PhoneModel = "PROP_POLICE_PHONE";

    private const string SWAT_ANIMATION_DICTIONARY = "cover@weapon@rpg";
    private const string SWAT_ANIMATION_LEFT = "blindfire_low_l_corner_exit";
    private const string SWAT_ANIMATION_RIGHT_LOOKING = "blindfire_low_r_corner_exit";
    private const string SWAT_ANIMATION_RIGHT = "blindfire_low_r_centre_exit";
    private const string HOSTAGE_ANIMATION_DICTIONARY = "random@arrests";
    private const string HOSTAGE_ANIMATION_KNEELING = "kneeling_arrest_idle";

    private readonly List<Vehicle> AllPoliceVehicles = [];
    private readonly List<Vehicle> AllRiot = [];
    private readonly List<Vehicle> AllAmbulance = [];
    private readonly List<Ped> AllOfficers = [];
    private readonly List<Ped> AllStandingOfficers = [];
    private readonly List<Ped> AllAimingOfficers = [];
    private readonly List<Ped> OfficersArresting = [];
    private readonly List<Ped> AllSWATUnits = [];
    private readonly List<Ped> AllRobbers = [];
    private readonly List<Ped> AllSneakRobbers = [];
    private readonly List<Ped> AllVaultRobbers = [];
    private readonly List<Entity> AllBarriersEntities = [];
    private readonly List<Ped> FightingSneakRobbers = [];
    private readonly List<Entity> CalloutEntities = [];
    private readonly List<Ped> RescuedHostages = [];
    private readonly List<Ped> SafeHostages = [];
    private readonly List<Ped> AllHostages = [];
    private readonly List<Ped> SpawnedHostages = [];
    private readonly List<Blip> RiotBlips = [];
    private readonly List<EnemyBlip> EnemyBlips = [];
    private int AliveHostagesCount = 0;
    private int SafeHostagesCount = 0;
    private int TotalHostagesCount = 0;

    private readonly RelationshipGroup RobbersRG = new("ROBBERS");
    private readonly RelationshipGroup SneakRobbersRG = new("SNEAK_ROBBERS");
    private readonly RelationshipGroup HostageRG = new("HOSTAGE");
    private Blip BankBlip;
    private Blip SideDoorBlip;
    private Ped Commander;
    private Ped Wife;
    private Ped WifeDriver;
    private Vehicle WifeCar;
    private Blip CommanderBlip;
    private RObject MobilePhone;

    private static int DiedRobbersCount = 0;

    private AlarmState CurrentAlarmState = AlarmState.None;
    private NegotiationResult Result = NegotiationResult.Fight;
    private bool IsAlarmPlaying = true;
    private bool AlarmStateChanged = true;
    private bool TalkedToCommander = false;
    private bool IsFighting = false;
    private bool SurrenderComplete = false;
    private bool IsSurrendering = false;
    private bool FightingPrepared = false;
    private bool IsSWATFollowing = false;
    private bool TalkedToCommander2nd = false;
    private bool DoneFighting = false;
    private bool EvaluatedWithWells = false;
    private bool RescuingHostage = false;
    private bool IsCalloutFinished = false;

    internal override void Setup()
    {
        CalloutPosition = BankLocation;
        CalloutMessage = Localization.GetString("PacificBankHeist");
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 30f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.PacificBankHeist, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("PacificBankHeistDesc"));

        IntroConversation =
        [
            (Settings.OfficerName, Localization.GetString("Intro1")),
            (Localization.GetString("Commander"), Localization.GetString("Intro2")),
            (Settings.OfficerName, Localization.GetString("Intro3")),
            (Localization.GetString("Commander"), Localization.GetString("Intro4", HostageCount.ToString())),
            (Localization.GetString("Commander"), Localization.GetString("Intro5")),
            (Localization.GetString("Commander"), Localization.GetString("Intro6")),
            (Localization.GetString("Commander"), Localization.GetString("Intro7")),
            (Localization.GetString("Commander"), Localization.GetString("Intro8")),
        ];

        OnCalloutsEnded += () =>
        {
            if (BankAlarm is not null)
            {
                BankAlarm.Stop();
                BankAlarm.Dispose();
            }
            Main.Player.IsPositionFrozen = false;
            Game.LocalPlayer.HasControl = true;
            // Main.Player.CanAttackFriendlies = false;
            NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 1f);
            NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 1f);
            NativeFunction.Natives.RESET_AI_WEAPON_DAMAGE_MODIFIER();
            NativeFunction.Natives.RESET_AI_MELEE_WEAPON_DAMAGE_MODIFIER();
            if (SideDoorBlip is not null && SideDoorBlip.IsValid() && SideDoorBlip.Exists()) SideDoorBlip.Delete();
            ToggleMobilePhone(Main.Player, false);

            if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists()) BankBlip.Delete();
            if (Commander is not null && Commander.IsValid() && Commander.Exists()) Commander.Dismiss();
            if (Wife is not null && Wife.IsValid() && Wife.Exists()) Wife.Dismiss();
            if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists()) WifeDriver.Dismiss();
            if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists()) WifeCar.Dismiss();
            if (CommanderBlip is not null && CommanderBlip.IsValid() && CommanderBlip.Exists()) CommanderBlip.Delete();
            if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists()) MobilePhone.Delete();
            foreach (var e in AllBarriersEntities) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
            foreach (var b in RiotBlips) if (b is not null && b.IsValid() && b.Exists()) b.Delete();
            foreach (var b in EnemyBlips) b?.Dismiss();
            World.RemoveSpeedZone(SpeedZoneId);

            if (IsCalloutFinished)
            {
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("PacificBankHeist"));

                foreach (var e in AllPoliceVehicles)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        var driver = e.HasDriver ? e.Driver : e.CreateRandomDriver();
                        if (driver is not null && driver.IsValid() && driver.Exists())
                        {
                            driver.Tasks.CruiseWithVehicle(e, 14f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                            driver.Dismiss();
                        }
                        e.Dismiss();
                    }
                }
                foreach (var e in AllAmbulance)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        var driver = e.HasDriver ? e.Driver : e.CreateRandomDriver();
                        if (driver is not null && driver.IsValid() && driver.Exists())
                        {
                            driver.Tasks.CruiseWithVehicle(e, 14f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                            driver.Dismiss();
                        }
                        e.Dismiss();
                    }
                }
                foreach (var e in AllHostages) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllStandingOfficers) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllAimingOfficers) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllSWATUnits) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllRobbers)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.IsAlive) e.Delete();
                        else e.Dismiss();
                    }
                }
                foreach (var e in AllSneakRobbers)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.IsAlive) e.Delete();
                        else e.Dismiss();
                    }
                }
                foreach (var e in AllVaultRobbers)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.IsAlive) e.Delete();
                        else e.Dismiss();
                    }
                }

                foreach (var e in CalloutEntities)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.GetType() == typeof(Vehicle))
                        {
                            var driver = ((Vehicle)e).HasDriver ? ((Vehicle)e).Driver : ((Vehicle)e).CreateRandomDriver();
                            if (driver is not null && driver.IsValid() && driver.Exists())
                            {
                                driver.Tasks.CruiseWithVehicle((Vehicle)e, 14f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                                driver.Dismiss();
                            }
                            e.Dismiss();
                        }
                        else
                        {
                            if (e.IsAlive) e.Delete();
                            else e.Dismiss();
                        }
                    }
                }
            }
            else
            {
                foreach (var e in AllPoliceVehicles) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllAmbulance) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllHostages) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllStandingOfficers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllAimingOfficers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllSWATUnits) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllRobbers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllSneakRobbers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllVaultRobbers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();

                foreach (var e in CalloutEntities) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("BankHeistWarning"), Main.PLUGIN_NAME, Localization.GetString("BankHeist"));
        BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{ALARM_SOUND_FILE_NAME}");
        BankAlarm.Load();
        NoLastRadio = true;
        if (Main.Player.IsInAnyVehicle(false))
        {
            CalloutEntities.Add(Main.Player.CurrentVehicle);
        }
        HudHelpers.DisplayNotification(Localization.GetString("PacificBankHeistDesc"), Localization.GetString("Dispatch"), Localization.GetString("BankHeist"));

        DiedHostagesTB = new(Localization.GetString("DiedHostages"), $"{(TotalHostagesCount - AliveHostagesCount).ToString()}")
        {
            Highlight = HudColor.Green.GetColor()
        };
        RescuedHostagesTB = new(Localization.GetString("RescuedHostages"), $"{SafeHostagesCount.ToString()}/{TotalHostagesCount.ToString()}")
        {
            Highlight = HudColor.Blue.GetColor()
        };
        TBPool.Add(DiedHostagesTB);
        TBPool.Add(RescuedHostagesTB);
        CalloutHandler();
    }

    internal override void NotAccepted() { }

    internal override void Update()
    {
        if (!IsCalloutRunning || DoneFighting)
        {
            Game.FrameRender -= TimerBarsProcess;
        }
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }

    private void CalloutHandler()
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                BankBlip = new(CalloutPosition)
                {
                    Sprite = SPRITE,
                    Color = Color.Yellow,
                    RouteColor = Color.Yellow,
                    IsRouteEnabled = true,
                };
                SideDoorBlip = new(new Vector3(258.3f, 200.4f, 104.9f))
                {
                    Sprite = SPRITE,
                    Color = Color.Yellow,
                };
                GameFiber.StartNew(() =>
                {
                    GameFiber.Wait(4800);
                    HudHelpers.DisplayNotification(Localization.GetString("BankHeistCopyThat"));
                    Functions.PlayScannerAudio("JP_COPY_THAT_MOVING_RIGHT_NOW REPORT_RESPONSE_COPY JP_PROCEED_WITH_CAUTION");
                    GameFiber.Wait(3400);
                    HudHelpers.DisplayNotification(Localization.GetString("BankHeistRoger"));
                });
                LoadModels();
                if (Main.Player.IsInAnyVehicle(false))
                {
                    CalloutEntities.Add(Main.Player.CurrentVehicle);
                    var passengers = Main.Player.CurrentVehicle.Passengers;
                    if (passengers.Length > 0)
                    {
                        foreach (var passenger in passengers)
                        {
                            CalloutEntities.Add(passenger);
                        }
                    }
                }
                GameFiber.WaitUntil(() => Vector3.Distance(Main.Player.Position, CalloutPosition) <= 200f);

                var weather = CalloutHelpers.GetWeatherType(IPTFunctions.GetWeatherType());

                SpeedZoneId = World.AddSpeedZone(CalloutPosition, 200f, 25f);
                GameFiber.Yield();
                ClearUnrelatedEntities();
                GameFiber.Yield();
                SpawnVehicles();
                GameFiber.Yield();
                PlaceBarrier();
                GameFiber.Yield();
                SpawnOfficers(weather);
                GameFiber.Yield();
                SpawnNegotiationRobbers(weather);
                GameFiber.Yield();
                SpawnSneakyRobbers(weather);
                GameFiber.Yield();
                SpawnEMS(weather);
                GameFiber.Yield();
                SpawnHostages(weather);
                GameFiber.Yield();
                MakeNearbyPedsFlee();

                SneakyRobbersAI();
                HandleHostages();
                HandleOpenBackRiotVan();
                HandleCalloutAudio();

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                    {
                        Main.Player.CanAttackFriendlies = false;
                        Main.Player.IsInvincible = false;
                        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobbersRG, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, RelationshipGroup.Cop, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RobbersRG, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Respect);
                        Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RelationshipGroup.Cop, Relationship.Respect);
                        Game.SetRelationshipBetweenRelationshipGroups(HostageRG, Main.Player.RelationshipGroup, Relationship.Respect);
                        Game.SetRelationshipBetweenRelationshipGroups(SneakRobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                        NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 0.45f);
                        NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 0.92f);
                        NativeFunction.Natives.SET_AI_MELEE_WEAPON_DAMAGE_MODIFIER(1f);
                        NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                        NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                        NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);
                    }

                    // When player has just arrived
                    if (!TalkedToCommander && !IsFighting)
                    {
                        if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                        {
                            if (!Main.Player.IsInAnyVehicle(false))
                            {
                                if (Commander is not null && Commander.IsValid() && Commander.IsAlive)
                                {
                                    if (Vector3.Distance(Main.Player.Position, Commander.Position) < 4f)
                                    {
                                        HudHelpers.DisplayNotification(Localization.GetString("BankHeistWarning"));
                                        KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                                        if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                        {
                                            TalkedToCommander = true;
                                            DetermineInitialDialogue();
                                        }
                                    }
                                    else
                                    {
                                        HudHelpers.DisplayHelp(Localization.GetString("TalkToCommander", $"~{COMMANDER_BLIP.GetIconToken()}~"));
                                    }
                                }
                            }
                        }
                    }

                    // Is fighting is initialized
                    if (!FightingPrepared)
                    {
                        if (IsFighting)
                        {
                            SpawnAssaultRobbers(weather);

                            if (Main.MersenneTwister.Next(10) is < 3)
                            {
                                SpawnVaultRobbers(weather);
                            }

                            // CopFightingAI();
                            // RobbersFightingAI();
                            Fighting();

                            foreach (var robber in AllRobbers)
                            {
                                var blip = new EnemyBlip(robber);
                                EnemyBlips.Add(blip);
                            }

                            FightingPrepared = true;
                        }
                    }

                    // If player talks to cpt wells during fight
                    if (IsFighting)
                    {
                        if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                        {
                            if (!Main.Player.IsInAnyVehicle(false))
                            {
                                if (Commander is not null && Commander.IsValid())
                                {
                                    if (Vector3.Distance(Main.Player.Position, Commander.Position) < 3f)
                                    {
                                        KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                                        if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                        {
                                            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("StillFighting"))]);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Make everyone fight if player enters bank
                    if (!IsFighting && !IsSurrendering)
                    {
                        foreach (var check in PacificBankInsideChecks)
                        {
                            if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                            {
                                if (Vector3.Distance(check, Main.Player.Position) < 2.3f)
                                {
                                    IsFighting = true;
                                }
                            }
                        }
                    }

                    // If all hostages rescued break
                    if (SafeHostagesCount == AliveHostagesCount) break;

                    // If surrendered
                    if (SurrenderComplete) break;
                    if (KeyHelpers.IsKeysDown(Settings.SWATFollowKey, Settings.SWATFollowModifierKey))
                    {
                        SwitchSWATFollowing();
                    }
                    if (IsSWATFollowing)
                    {
                        if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                        {
                            if (Main.Player.IsShooting)
                            {
                                IsSWATFollowing = false;
                                HudHelpers.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
                                Logger.Info("Follow off - shooting", "Bank Heist");
                            }
                        }
                    }
                }

                // When surrendered
                if (SurrenderComplete) CopsReturnToLocation();

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    // Constants
                    if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                    {
                        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobbersRG, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, RelationshipGroup.Cop, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RobbersRG, Relationship.Hate);
                        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Companion);
                        Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RelationshipGroup.Cop, Relationship.Companion);
                        Game.SetRelationshipBetweenRelationshipGroups(HostageRG, Main.Player.RelationshipGroup, Relationship.Companion);
                        Game.SetRelationshipBetweenRelationshipGroups(SneakRobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                        Main.Player.IsInvincible = false;
                        NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 0.45f);
                        NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 0.93f);
                        NativeFunction.Natives.SET_AI_MELEE_WEAPON_DAMAGE_MODIFIER(1f);
                    }
                    NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                    NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                    NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);
                    // If all hostages rescued
                    if (SafeHostagesCount == AliveHostagesCount)
                    {
                        GameFiber.Wait(3000);
                        break;
                    }
                    if (KeyHelpers.IsKeysDown(Settings.SWATFollowKey, Settings.SWATFollowModifierKey))
                    {
                        SwitchSWATFollowing();
                    }
                    if (IsSWATFollowing)
                    {
                        if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                        {
                            if (Main.Player.IsShooting)
                            {
                                IsSWATFollowing = false;
                                HudHelpers.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
                                Logger.Info("Follow off - shooting", "Bank Heist");
                            }
                        }
                    }

                    if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
                    {
                        if (!Main.Player.IsInAnyVehicle(false))
                        {
                            if (Commander is not null && Commander.IsValid())
                            {
                                if (Vector3.Distance(Main.Player.Position, Commander.Position) < 4f)
                                {
                                    KeyHelpers.DisplayKeyHelp("PressToTalk", [Localization.GetString("Commander")], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                                    if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                    {
                                        if (!TalkedToCommander2nd)
                                        {
                                            Conversations.Talk(AfterSurrendered);
                                            TalkedToCommander2nd = true;
                                            IsFighting = true;
                                            KeyHelpers.DisplayKeyHelp("SWATFollowing", Settings.SWATFollowKey, Settings.SWATFollowModifierKey);
                                        }
                                        else
                                        {
                                            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("StillFighting"))]);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!TalkedToCommander2nd)
                                    {
                                        HudHelpers.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"));
                                    }
                                }
                            }
                        }
                    }
                }

                // The end
                IsSWATFollowing = false;
                DoneFighting = true;
                CurrentAlarmState = AlarmState.None;
                AlarmStateChanged = true;
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                    NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                    NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);
                    if (!EvaluatedWithWells)
                    {
                        if (!Main.Player.IsInAnyVehicle(false))
                        {
                            if (Vector3.Distance(Main.Player.Position, Commander.Position) < 4f)
                            {
                                KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                {
                                    TalkedToCommander = true;
                                    Conversations.Talk(Final);
                                    GameFiber.Wait(5000);
                                    DetermineResults();
                                    EvaluatedWithWells = true;
                                    GameFiber.Wait(2500);
                                    break;
                                }
                            }
                            else
                            {
                                HudHelpers.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"));
                            }
                        }
                    }
                }
                Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH JP_WE_ARE_CODE JP_FOUR JP_NO_FURTHER_UNITS_REQUIRED");
                IsCalloutFinished = true;
                End();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        });
    }

    private void LoadModels()
    {
        GameFiber.Yield();
        BarrierModel.Load();
        PhoneModel.Load();
    }

    private void DetermineResults()
    {
        foreach (var robber in AllRobbers)
        {
            if (robber is not null && robber.IsValid() && robber.Exists() && robber.IsDead)
            {
                DiedRobbersCount++;
            }
        }
        HudHelpers.DisplayNotification(Localization.GetString("BankHeistReportText", $"{SafeHostagesCount.ToString()}", $"{(TotalHostagesCount - AliveHostagesCount).ToString()}", $"{DiedRobbersCount.ToString()}"), Localization.GetString("BankHeistReportTitle"), TotalHostagesCount - AliveHostagesCount is < 3 ? Localization.GetString("BankHeistReportSubtitle") : "", "mphud", "mp_player_ready");
        if (TotalHostagesCount == AliveHostagesCount)
        {
            var bigMessage = new BigMessageThread(true);
            bigMessage.MessageInstance.ShowColoredShard(Localization.GetString("Congratulations"), Localization.GetString("NoDiedHostage"), HudColor.Yellow, HudColor.MenuGrey);
            Common.PlaySound("Mission_Pass_Notify", "DLC_HEISTS_GENERAL_FRONTEND_SOUNDS");
        }
    }

    private void SwitchSWATFollowing()
    {
        IsSWATFollowing = !IsSWATFollowing;
        if (IsSWATFollowing)
        {
            HudHelpers.DisplayHelp(Localization.GetString("SWATIsFollowing"));
        }
        else
        {
            HudHelpers.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
        }
    }

    private void Fighting()
    {
        GameFiber.StartNew(() =>
        {
            foreach (var cop in AllOfficers)
            {
                cop.Tasks.FightAgainstClosestHatedTarget(500f, -1);
            }
            foreach (var cop in AllSWATUnits)
            {
                cop.Tasks.FightAgainstClosestHatedTarget(500f, -1);
            }
            foreach (var robber in AllRobbers)
            {
                robber.Tasks.FightAgainstClosestHatedTarget(500f, -1);
            }
        });
    }

    // private GameFiber CopFightingAIGameFiber;
    // private void CopFightingAI()
    // {
    //     CopFightingAIGameFiber = GameFiber.StartNew(() =>
    //     {
    //         while (IsCalloutRunning)
    //         {
    //             try
    //             {
    //                 GameFiber.Yield();
    //                 if (IsFighting)
    //                 {
    //                     if (OfficersTargetsToShoot.Count > 0)
    //                     {
    //                         if (OfficersTargetsToShoot[0] is not null && OfficersTargetsToShoot[0].IsValid() && OfficersTargetsToShoot[0].Exists())
    //                         {
    //                             if (OfficersTargetsToShoot[0].IsAlive)
    //                             {
    //                                 foreach (var cop in OfficersTargetsToShoot)
    //                                 {
    //                                     cop.Tasks.FightAgainst(OfficersTargetsToShoot[0]);
    //                                 }
    //                             }
    //                             else
    //                             {
    //                                 OfficersTargetsToShoot.RemoveAt(0);
    //                             }
    //                         }
    //                         else
    //                         {
    //                             OfficersTargetsToShoot.RemoveAt(0);
    //                         }
    //                     }
    //                     else
    //                     {
    //                         CopsReturnToLocation();
    //                     }
    //                 }
    //                 if (IsFighting || IsSWATFollowing)
    //                 {
    //                     foreach (var cop in AllSWATUnits)
    //                     {
    //                         GameFiber.Yield();
    //                         if (cop is not null && cop.IsValid() && cop.Exists())
    //                         {
    //                             if (!IsSWATFollowing)
    //                             {
    //                                 NativeFunction.Natives.REGISTER_HATED_TARGETS_AROUND_PED(cop, 60f);
    //                                 cop.Tasks.FightAgainstClosestHatedTarget(60f);
    //                             }
    //                             else
    //                             {
    //                                 if (Main.Player is not null && Main.Player.IsValid() && Main.Player.IsAlive)
    //                                 {
    //                                     cop.Tasks.FollowNavigationMeshToPosition(Main.Player.Position, Main.Player.Heading, 1.6f, Math.Abs(Main.Player.Position.Z - cop.Position.Z) > 1f ? 1f : 4f);
    //                                 }
    //                             }
    //                         }
    //                     }
    //                     GameFiber.Sleep(4000);
    //                 }
    //             }
    //             catch (Exception e)
    //             {
    //                 Logger.Error(e.ToString());
    //             }
    //         }
    //     });
    // }

    // private Entity entityPlayerAimingAt;
    // private void RobbersFightingAI()
    // {
    //     GameFiber.StartNew(() =>
    //     {
    //         while (IsCalloutRunning)
    //         {
    //             try
    //             {
    //                 GameFiber.Yield();
    //                 if (IsFighting)
    //                 {
    //                     foreach (var robber in AllRobbers)
    //                     {
    //                         GameFiber.Yield();
    //                         if (robber is not null && robber.IsValid() && robber.Exists())
    //                         {
    //                             var distance = Vector3.Distance(robber.Position, PacificBankInsideChecks[0]) < Vector3.Distance(robber.Position, PacificBankInsideChecks[1]) ? Vector3.Distance(robber.Position, PacificBankInsideChecks[0]) : Vector3.Distance(robber.Position, PacificBankInsideChecks[1]);
    //                             if (distance < 16.5f) distance = 16.5f;
    //                             else if (distance > 21f) distance = 21f;
    //                             NativeFunction.Natives.REGISTER_HATED_TARGETS_AROUND_PED(robber, distance);
    //                             robber.Tasks?.FightAgainstClosestHatedTarget(distance);
    //                             // Rage.Native.NativeFunction.CallByName<uint>("TASK_GUARD_CURRENT_POSITION", robber, 10.0f, 10.0f, true);
    //                             try
    //                             {
    //                                 unsafe
    //                                 {
    //                                     uint entityHandle;
    //                                     NativeFunction.Natives.x2975C866E6713290(Game.LocalPlayer, new IntPtr(&entityHandle)); // Stores the entity the player is aiming at in the uint provided in the second parameter.
    //                                     entityPlayerAimingAt = World.GetEntityByHandle<Entity>(entityHandle);
    //                                 }
    //                             }
    //                             catch // (Exception e)
    //                             {
    //                                 // Logger.Error(e.ToString());
    //                             }

    //                             if (AllRobbers.Contains(entityPlayerAimingAt))
    //                             {
    //                                 var pedAimingAt = (Ped)entityPlayerAimingAt;
    //                                 pedAimingAt.Tasks.FightAgainst(Main.Player);
    //                             }
    //                         }
    //                     }
    //                     GameFiber.Sleep(3000);
    //                 }
    //             }
    //             catch (Exception e)
    //             {
    //                 Logger.Error(e.ToString());
    //             }
    //         }
    //     });
    // }

    private void CopsReturnToLocation()
    {
        for (int i = 0; i < AllStandingOfficers.Count; i++)
        {
            if (AllStandingOfficers[i] is not null && AllStandingOfficers[i].IsValid() && AllStandingOfficers[i].Exists())
            {
                if (AllStandingOfficers[i].IsAlive)
                {
                    var data = XmlManager.PacificBankHeistConfig.StandingOfficerPositions[i];
                    var pos = new Vector3(data.X, data.Y, data.Z);
                    if (Vector3.Distance(AllStandingOfficers[i].Position, new Vector3(data.X, data.Y, data.Z)) > 0.5f)
                    {
                        AllStandingOfficers[i].BlockPermanentEvents = true;
                        AllStandingOfficers[i].Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 2f);
                    }
                }
            }
        }
        for (int i = 0; i < AllAimingOfficers.Count; i++)
        {
            if (AllAimingOfficers is not null && AllAimingOfficers[i].IsValid() && AllAimingOfficers[i].Exists())
            {
                if (AllAimingOfficers[i].IsAlive)
                {
                    var data = XmlManager.PacificBankHeistConfig.AimingOfficerPositions[i];
                    var pos = new Vector3(data.X, data.Y, data.Z);
                    if (Vector3.Distance(AllAimingOfficers[i].Position, pos) > 0.5f)
                    {
                        AllAimingOfficers[i].BlockPermanentEvents = true;
                        AllAimingOfficers[i].Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 2f);
                    }
                    else
                    {
                        var aimPoint = Vector3.Distance(AllAimingOfficers[i].Position, BankDoorPositions[0]) < Vector3.Distance(AllAimingOfficers[i].Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
                        NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(AllAimingOfficers[i], aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);
                    }
                }
            }
        }
    }

    private void DetermineInitialDialogue()
    {
        Conversations.Talk(IntroConversation);
        var intro = Conversations.DisplayAnswersForCallout(IntroSelection);
        // Try to discuss
        if (intro is 0)
        {
            Conversations.Talk(DiscussConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4000);
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                KeyHelpers.DisplayKeyHelp("CallBankRobbers", Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey)) break;
            }
            Game.HideHelp();
            NegotiationIntro();
        }
        // Fighting
        else if (intro is 1)
        {
            Conversations.Talk(FightConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4500);
            PrepareFighting();
        }
    }

    private void SwitchAlarmQuestion()
    {
        int alarm = Conversations.DisplayAnswersForCallout(AlarmSelection);
        if (alarm is 0)
        {
            Conversations.Talk(AlarmOffConversation);
            BankAlarm.Stop();
            CurrentAlarmState = AlarmState.None;
            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("Alarm5"))]);
        }
        else if (alarm is 1)
        {
            CurrentAlarmState = AlarmState.Alarm;
            Conversations.Talk(AlarmOnConversation);
        }
        KeyHelpers.DisplayKeyHelp("AlarmSwitchKey", Settings.ToggleBankHeistAlarmSoundKey, Settings.ToggleBankHeistAlarmSoundModifierKey);
    }

    private void NegotiationIntro()
    {
        ToggleMobilePhone(Main.Player, true);
        Conversations.PlayPhoneCallingSound(2);
        GameFiber.Wait(5800);
        Result = NegotiationResult.Fight;
        Conversations.Talk(NegotiationIntroConversation);
        var intro = Conversations.DisplayAnswersForCallout(NegotiationIntroSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(Negotiation1Conversation);
                var ng1 = Conversations.DisplayAnswersForCallout(Negotiation1Selection);
                switch (ng1)
                {
                    default:
                    case 0:
                        Conversations.Talk(Negotiation11Conversation);
                        var ng11 = Conversations.DisplayAnswersForCallout(Negotiation11Selection);
                        switch (ng11)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation111Conversation);
                                Result = NegotiationResult.Fight;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation112Conversation);
                                Result = NegotiationResult.Fight;
                                break;
                            case 2:
                                Conversations.Talk(Negotiation113Conversation);
                                Result = NegotiationResult.Surrender;
                                break;
                        }
                        break;
                    case 1:
                        Request();
                        break;
                    case 2:
                        Conversations.Talk(Negotiation13Conversation);
                        var ng13 = Conversations.DisplayAnswersForCallout(Negotiation13Selection);
                        switch (ng13)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation131Conversation);
                                Result = NegotiationResult.Fight;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation111Conversation);
                                Result = NegotiationResult.Fight;
                                break;
                            case 2:
                                var isSucceed = Main.MersenneTwister.Next(2) is 0;
                                if (isSucceed)
                                {
                                    Conversations.Talk(Negotiation133Conversation1);
                                    Result = NegotiationResult.Surrender;
                                }
                                else
                                {
                                    Conversations.Talk(Negotiation133Conversation2);
                                    Result = NegotiationResult.Fight;
                                }
                                break;
                        }
                        break;
                }
                break;
            case 1:
                Request();
                break;
            case 2:
                Conversations.Talk(Negotiation3Conversation);
                var ng3 = Conversations.DisplayAnswersForCallout(Negotiation3Selection);
                switch (ng3)
                {
                    default:
                    case 0:
                        Conversations.Talk(Negotiation31Conversation);
                        Result = NegotiationResult.Fight;
                        break;
                    case 1:
                        Conversations.Talk(Negotiation32Conversation);
                        var ng32 = Conversations.DisplayAnswersForCallout(Negotiation32Selection);
                        switch (ng32)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation321Conversation);
                                Result = NegotiationResult.Surrender;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation322Conversation);
                                Result = NegotiationResult.Fight;
                                break;
                            case 2:
                                Conversations.Talk(Negotiation323Conversation);
                                Result = NegotiationResult.Fight;
                                break;
                        }
                        break;
                    case 2:
                        Conversations.Talk(Negotiation33Conversation);
                        Result = NegotiationResult.Fight;
                        break;
                    case 3:
                        Conversations.Talk(Negotiation34Conversation);
                        Result = NegotiationResult.Surrender;
                        break;
                }
                break;
        }

        if (!Wife.Exists())
        {
            Conversations.PlayPhoneBusySound(1);
        }
        ToggleMobilePhone(Main.Player, false);

        GameFiber.Wait(2000);
        if (Result is NegotiationResult.Surrender)
        {
            NegotiationRobbersSurrender();
            return;
        }
        else
        {
            PrepareFighting();
        }
    }

    private void PrepareFighting()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();
            KeyHelpers.DisplayKeyHelp("BankHeistMoveIn", Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
            {
                Conversations.Talk([(Settings.OfficerName, Localization.GetString("MoveIn"))]);
                KeyHelpers.DisplayKeyHelp("SWATFollowing", Settings.SWATFollowKey, Settings.SWATFollowModifierKey);
                IsFighting = true;
                break;
            }
        }
    }

    private void Request()
    {
        Conversations.Talk(RequestConversation);
        int intro = Conversations.DisplayAnswersForCallout(RequestSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(Request1Conversation);
                Result = NegotiationResult.Fight;
                break;
            case 1:
                Conversations.Talk(Request2Conversation);
                int req2 = Conversations.DisplayAnswersForCallout(Request2Selection);
                switch (req2)
                {
                    default:
                    case 0:
                        Conversations.Talk(Request21Conversation);
                        Result = NegotiationResult.Fight;
                        break;
                    case 1:
                        var isSucceed = Main.MersenneTwister.Next(2) is 1;
                        // var isSucceed = true;
                        if (isSucceed)
                        {
                            Conversations.Talk(Request22Conversation2);
                            GetWife();
                            ToggleMobilePhone(Main.Player, false);
                            ToggleMobilePhone(Wife, true);
                            Conversations.Talk(Request22Conversation3);
                            Conversations.PlayPhoneBusySound(1);
                            ToggleMobilePhone(Wife, false);
                            GameFiber.Wait(1500);

                            Conversations.Talk(Request22Conversation4);
                            Wife.Tasks.FollowNavigationMeshToPosition(WifeCar.GetOffsetPosition(Vector3.RelativeRight * 2f), WifeCar.Heading, 1.9f).WaitForCompletion(15000);
                            Wife.Tasks.EnterVehicle(WifeCar, 5000, 0).WaitForCompletion();
                            GameFiber.StartNew(() =>
                            {
                                WifeDriver.Tasks.CruiseWithVehicle(WifeCar, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds).WaitForCompletion();
                                if (Wife is not null && Wife.IsValid() && Wife.Exists()) Wife.Delete();
                                if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists()) WifeDriver.Delete();
                                if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists()) WifeCar.Delete();
                            });
                            Result = NegotiationResult.Surrender;
                        }
                        else
                        {
                            Conversations.Talk(Request22Conversation1);
                            Result = NegotiationResult.Fight;
                        }
                        break;
                    case 2:
                        Conversations.Talk([(Settings.OfficerName, Localization.GetString("Request231"))]);
                        Result = NegotiationResult.Fight;
                        break;
                }
                break;
            case 2:
                Conversations.Talk(Request3Conversation);
                Result = NegotiationResult.Fight;
                break;
        }
    }

    private void NegotiationRobbersSurrender()
    {
        SurrenderComplete = false;
        IsSurrendering = true;
        GameFiber.StartNew(() =>
        {
            try
            {
                HudHelpers.DisplayNotification($"~b~{Localization.GetString("Commander")}~s~: {Localization.GetString("SeemSurrender")}");
                GameFiber.Wait(5000);
                HudHelpers.DisplayHelp(Localization.GetString("SurrenderHelp"), 80000);
                bool AllRobbersAtLocation = false;
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    GameFiber.Yield();
                    var data = XmlManager.PacificBankHeistConfig.RobbersSurrenderingPositions[i];
                    var pos = new Vector3(data.X, data.Y, data.Z);
                    AllRobbers[i].Tasks.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 1f, AnimationFlags.UpperBodyOnly | AnimationFlags.StayInEndFrame | AnimationFlags.SecondaryTask);
                    AllRobbers[i].Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.45f);
                    NativeFunction.Natives.SET_PED_CAN_RAGDOLL(AllRobbers[i], false);
                }
                int count = 0;
                while (!AllRobbersAtLocation)
                {
                    GameFiber.Yield();
                    count++;
                    if (count >= 10000)
                    {
                        for (int i = 0; i < AllRobbers.Count; i++)
                        {
                            var data = XmlManager.PacificBankHeistConfig.RobbersSurrenderingPositions[i];
                            var pos = new Vector3(data.X, data.Y, data.Z);
                            AllRobbers[i].Position = pos;
                            AllRobbers[i].Heading = data.Heading;
                        }
                        break;
                    }
                    for (int i = 0; i < AllRobbers.Count; i++)
                    {
                        GameFiber.Yield();
                        var data = XmlManager.PacificBankHeistConfig.RobbersSurrenderingPositions[i];
                        var pos = new Vector3(data.X, data.Y, data.Z);
                        if (Vector3.Distance(AllRobbers[i].Position, pos) < 0.8f)
                        {
                            AllRobbersAtLocation = true;
                        }
                        else
                        {
                            AllRobbersAtLocation = false;
                            break;
                        }
                    }
                    foreach (var swat in AllSWATUnits)
                    {
                        GameFiber.Yield();
                        var robber = AllRobbers[Main.MersenneTwister.Next(AllRobbers.Count)];
                        NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(swat, robber.Position.X, robber.Position.Y, robber.Position.Z, -1, false, false);
                    }
                }
                GameFiber.Wait(1000);
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    GameFiber.Yield();

                    AllRobbers[i].Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                    NativeFunction.Natives.SET_PED_DROPS_WEAPON(AllRobbers[i]);
                    if (AllOfficers.Count > i)
                    {
                        OfficersArresting.Add(AllOfficers[i]);
                        AllOfficers[i].Tasks.FollowNavigationMeshToPosition(AllRobbers[i].GetOffsetPosition(Vector3.RelativeBack * 0.7f), AllRobbers[i].Heading, 1.55f);
                        NativeFunction.Natives.SET_PED_CAN_RAGDOLL(AllOfficers[i], false);
                    }
                }
                GameFiber.Wait(1000);
                bool AllArrestingOfficersAtLocation = false;
                count = 0;
                while (!AllArrestingOfficersAtLocation)
                {
                    GameFiber.Yield();
                    count++;
                    if (count >= 10000)
                    {
                        for (int i = 0; i < OfficersArresting.Count; i++)
                        {
                            OfficersArresting[i].Position = AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f);
                            OfficersArresting[i].Heading = AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].Heading;
                        }
                        break;
                    }
                    for (int i = 0; i < OfficersArresting.Count; i++)
                    {
                        if (Vector3.Distance(OfficersArresting[i].Position, AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f)) < 0.8f)
                        {
                            AllArrestingOfficersAtLocation = true;
                        }
                        else
                        {
                            OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f), AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].Heading, 1.55f).WaitForCompletion(500);
                            AllArrestingOfficersAtLocation = false;
                            break;
                        }
                    }
                }
                foreach (var swat in AllSWATUnits)
                {
                    swat.Tasks.Clear();
                }
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    AllRobbers[i].Tasks.PlayAnimation("mp_arresting", "idle", 8f, AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask | AnimationFlags.Loop);
                    AllRobbers[i].Tasks.FollowNavigationMeshToPosition(AllPoliceVehicles[i].GetOffsetPosition(Vector3.RelativeLeft * 2f), AllPoliceVehicles[i].Heading, 1.58f);
                    OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(AllPoliceVehicles[i].GetOffsetPosition(Vector3.RelativeLeft * 2f), AllPoliceVehicles[i].Heading, 1.55f);
                }
                GameFiber.Wait(5000);
                SurrenderComplete = true;
                GameFiber.Wait(12000);
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    AllRobbers[i].BlockPermanentEvents = true;
                    AllRobbers[i].Tasks.EnterVehicle(AllPoliceVehicles[i], 11000, 1);
                    OfficersArresting[i].BlockPermanentEvents = true;
                    OfficersArresting[i].Tasks.EnterVehicle(AllPoliceVehicles[i], 11000, -1);
                }
                GameFiber.Wait(11100);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        });
    }

    private void SpawnVehicles()
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.PoliceCruiserPositions)
        {
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceCruisers]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllPoliceVehicles.Add(vehicle);
                CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.PoliceTransportPositions)
        {
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceTransporters]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllPoliceVehicles.Add(vehicle);
                CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.RiotPositions)
        {
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceRiots]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllPoliceVehicles.Add(vehicle);
                AllRiot.Add(vehicle);
                CalloutEntities.Add(vehicle);

                var blip = new Blip(vehicle)
                {
                    Sprite = RIOT_BLIP,
                    Color = HudColor.Michael.GetColor(),
                };
                if (blip is not null && blip.IsValid() && blip.Exists())
                {
                    RiotBlips.Add(blip);
                }
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.AmbulancePositions)
        {
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.Ambulances]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllAmbulance.Add(vehicle);
                CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.FiretruckPositions)
        {
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.Firetrucks]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                CalloutEntities.Add(vehicle);
            }
        }
    }

    private void PlaceBarrier()
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.BarrierPositions)
        {
            var barrier = new RObject(BarrierModel, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPositionFrozen = true,
                IsPersistent = true
            };
            // var invisibleWall = new RObject(InvisibleWallModel, barrier.Position, p.Heading)
            // {
            //     IsVisible = false,
            //     IsPersistent = true
            // };
            var barrierPed = new Ped(barrier.Position)
            {
                IsVisible = false,
                IsPositionFrozen = true,
                BlockPermanentEvents = true,
                IsPersistent = true
            };
            AllBarriersEntities.Add(barrier);
            AllBarriersEntities.Add(barrierPed);
        }
    }

    private void SpawnOfficers(EWeather weather)
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.LeftSittingSWATPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceSWATModels]);
            var swat = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (swat is not null && swat.IsValid() && swat.Exists())
            {
                NativeFunction.Natives.SET_PED_KEEP_TASK(swat, true);
                swat.SetOutfit(data);
                Functions.SetPedAsCop(swat);
                Functions.SetCopAsBusy(swat, true);
                swat.GiveWeapon([.. XmlManager.PacificBankHeistConfig.SWATWeapons], true);
                swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);
                AllSWATUnits.Add(swat);
                CalloutEntities.Add(swat);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.RightLookingSWATPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceSWATModels]);
            var swat = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (swat is not null && swat.IsValid() && swat.Exists())
            {
                NativeFunction.Natives.SET_PED_KEEP_TASK(swat, true);
                swat.SetOutfit(data);
                Functions.SetPedAsCop(swat);
                Functions.SetCopAsBusy(swat, true);
                swat.GiveWeapon([.. XmlManager.PacificBankHeistConfig.SWATWeapons], true);
                swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT_LOOKING, 1f, AnimationFlags.StayInEndFrame);
                AllSWATUnits.Add(swat);
                CalloutEntities.Add(swat);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.RightSittingSWATPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceSWATModels]);
            var swat = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (swat is not null && swat.IsValid() && swat.Exists())
            {
                NativeFunction.Natives.SET_PED_KEEP_TASK(swat, true);
                swat.SetOutfit(data);
                Functions.SetPedAsCop(swat);
                Functions.SetCopAsBusy(swat, true);
                swat.GiveWeapon([.. XmlManager.PacificBankHeistConfig.SWATWeapons], true);
                swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 1f, AnimationFlags.StayInEndFrame);
                AllSWATUnits.Add(swat);
                CalloutEntities.Add(swat);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.AimingOfficerPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceOfficerModels]);
            var officer = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (officer is not null && officer.IsValid() && officer.Exists())
            {
                NativeFunction.Natives.SET_PED_KEEP_TASK(officer, true);
                officer.SetOutfit(data);
                Functions.SetPedAsCop(officer);
                Functions.SetCopAsBusy(officer, true);
                officer.GiveWeapon([.. XmlManager.PacificBankHeistConfig.OfficerWeapons], true);
                var aimPoint = Vector3.Distance(officer.Position, BankDoorPositions[0]) < Vector3.Distance(officer.Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
                NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(officer, aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);

                AllOfficers.Add(officer);
                AllAimingOfficers.Add(officer);
                CalloutEntities.Add(officer);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.StandingOfficerPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceOfficerModels]);
            var officer = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (officer is not null && officer.IsValid() && officer.Exists())
            {
                NativeFunction.Natives.SET_PED_KEEP_TASK(officer, true);
                officer.SetOutfit(data);
                Functions.SetPedAsCop(officer);
                Functions.SetCopAsBusy(officer, true);
                officer.GiveWeapon([.. XmlManager.PacificBankHeistConfig.OfficerWeapons], true);

                AllOfficers.Add(officer);
                AllStandingOfficers.Add(officer);
                CalloutEntities.Add(officer);
            }
        }
        var cP = XmlManager.PacificBankHeistConfig.CommanderPosition;
        var cData = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.CommanderModels]);
        Commander = new Ped(cData.Model, new(cP.X, cP.Y, cP.Z), cP.Heading)
        {
            BlockPermanentEvents = true,
            IsPersistent = true,
            IsInvincible = true,
            RelationshipGroup = RelationshipGroup.Cop,
            MaxHealth = cData.Health,
            Health = cData.Health,
            Armor = cData.Armor,
        };
        if (Commander is not null && Commander.IsValid() && Commander.Exists())
        {
            Commander.SetOutfit(cData);
            Functions.SetPedCantBeArrestedByPlayer(Commander, true);

            CommanderBlip = Commander.AttachBlip();
            if (CommanderBlip is not null && CommanderBlip.IsValid() && CommanderBlip.Exists())
            {
                CommanderBlip.Sprite = COMMANDER_BLIP;
                CommanderBlip.Color = Color.Green;
            }
            CalloutEntities.Add(Commander);
        }
    }

    private void SpawnEMS(EWeather weather)
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.ParamedicPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.ParamedicModels]);
            var paramedic = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (paramedic is not null && paramedic.IsValid() && paramedic.Exists())
            {
                paramedic.SetOutfit(data);
                CalloutEntities.Add(paramedic);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.FirefighterPositions)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.FirefighterModels]);
            var firefighter = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (firefighter is not null && firefighter.IsValid() && firefighter.Exists())
            {
                firefighter.SetOutfit(data);
                CalloutEntities.Add(firefighter);
            }
        }
    }

    private void SpawnHostages(EWeather weather)
    {
        var positions = XmlManager.PacificBankHeistConfig.HostagePositions.Shuffle();
        for (int i = 0; i < HostageCount; i++)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.HostageModels]);
            var pos = new Vector3(positions[i].X, positions[i].Y, positions[i].Z);
            var hostage = new Ped(data.Model, pos, Main.MersenneTwister.Next(0, 360))
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = HostageRG,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (hostage is not null && hostage.IsValid() && hostage.Exists())
            {
                NativeFunction.Natives.SET_PED_CAN_RAGDOLL(hostage, false);
                hostage.SetOutfit(data);
                AllHostages.Add(hostage);
                SpawnedHostages.Add(hostage);
                CalloutEntities.Add(hostage);
                hostage.Tasks.PlayAnimation(HOSTAGE_ANIMATION_DICTIONARY, HOSTAGE_ANIMATION_KNEELING, 1f, AnimationFlags.Loop);
                GameFiber.Yield();
                AliveHostagesCount++;
                TotalHostagesCount++;
            }
        }
    }

    private void SpawnAssaultRobbers(EWeather weather)
    {
        var nrP = XmlManager.PacificBankHeistConfig.NormalRobbersPositions;
        for (int i = 0; i < nrP.Count; i++)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
            var ped = new Ped(data.Model, new(nrP[i].X, nrP[i].Y, nrP[i].Z), nrP[i].Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
                RelationshipGroup = RobbersRG,
            };
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                ped.SetOutfit(data);
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersThrowableWeapons], false);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                NativeFunction.Natives.SetPedCombatAbility(ped, 3);
                AllRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
        }
    }

    private void SpawnVaultRobbers(EWeather weather)
    {
        for (int i = 0; i < XmlManager.PacificBankHeistConfig.RobbersInVaultPositions.Count; i++)
        {
            var rvP = XmlManager.PacificBankHeistConfig.RobbersInVaultPositions[i];
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
            var ped = new Ped(data.Model, new(rvP.X, rvP.Y, rvP.Z), rvP.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
                RelationshipGroup = RobbersRG,
            };
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                ped.SetOutfit(data);
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersThrowableWeapons], false);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                NativeFunction.Natives.SetPedCombatAbility(ped, 3);
                AllVaultRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
        }
        HandleVaultRobbers();
    }

    private void SpawnNegotiationRobbers(EWeather weather)
    {
        for (int i = 0; i < XmlManager.PacificBankHeistConfig.RobbersNegotiationPositions.Count; i++)
        {
            var rnP = XmlManager.PacificBankHeistConfig.RobbersNegotiationPositions[i];
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
            var ped = new Ped(data.Model, new(rnP.X, rnP.Y, rnP.Z), rnP.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
                RelationshipGroup = RobbersRG,
            };
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                ped.SetOutfit(data);
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                NativeFunction.Natives.SetPedCombatAbility(ped, 3);
                AllRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
        }
    }

    private void SpawnSneakyRobbers(EWeather weather)
    {
        for (int i = 0; i < XmlManager.PacificBankHeistConfig.RobbersSneakPosition.Count; i++)
        {
            var rsP = XmlManager.PacificBankHeistConfig.RobbersSneakPosition[i];
            if (Main.MersenneTwister.Next(5) is >= 2)
            {
                var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
                var ped = new Ped(data.Model, new(rsP.X, rsP.Y, rsP.Z), rsP.Heading)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    CanAttackFriendlies = false,
                    MaxHealth = data.Health,
                    Health = data.Health,
                    Armor = data.Armor,
                    RelationshipGroup = SneakRobbersRG
                };
                if (ped is not null && ped.IsValid() && ped.Exists())
                {
                    ped.SetOutfit(data);
                    Functions.SetPedCantBeArrestedByPlayer(ped, true);
                    ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                    NativeFunction.Natives.SetPedCombatAbility(ped, 3);
                    ped.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, rsP.IsRight ? SWAT_ANIMATION_RIGHT : SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);
                    AllSneakRobbers.Add(ped);
                    CalloutEntities.Add(ped);
                }
            }
            else
            {
                AllSneakRobbers.Add(null);
            }
        }
    }

    private void HandleVaultRobbers()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                try
                {
                    if (Vector3.Distance(Main.Player.Position, OutsideBankVault) < 4f)
                    {
                        GameFiber.Wait(2000);

                        AllVaultRobbers[2].Tasks.FollowNavigationMeshToPosition(OutsideBankVault, AllVaultRobbers[2].Heading, 2f).WaitForCompletion(500);
                        World.SpawnExplosion(new Vector3(252.2609f, 225.3824f, 101.6835f), 2, 0.2f, true, false, 0.6f);
                        CurrentAlarmState = AlarmState.Alarm;
                        AlarmStateChanged = true;
                        GameFiber.Wait(900);
                        foreach (var robber in AllVaultRobbers)
                        {
                            robber.Tasks.FightAgainstClosestHatedTarget(23f);
                        }
                        GameFiber.Wait(3000);
                        foreach (Ped robber in AllVaultRobbers)
                        {
                            AllRobbers.Add(robber);
                        }
                        break;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private void HandleCalloutAudio()
    {
        GameFiber.StartNew(() =>
        {
            CurrentAlarmState = AlarmState.None;
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    if (IsAlarmPlaying)
                    {
                        if (Vector3.Distance(Main.Player.Position, CalloutPosition) > 70f)
                        {
                            IsAlarmPlaying = false;
                            CurrentAlarmState = AlarmState.None;
                            BankBlip.IsRouteEnabled = true;
                            AlarmStateChanged = true;
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(Main.Player.Position, CalloutPosition) < 55f)
                        {
                            IsAlarmPlaying = true;
                            CurrentAlarmState = AlarmState.Alarm;
                            BankBlip.IsRouteEnabled = false;
                            AlarmStateChanged = true;
                        }
                    }

                    if (KeyHelpers.IsKeysDown(Settings.ToggleBankHeistAlarmSoundKey, Settings.ToggleBankHeistAlarmSoundModifierKey))
                    {
                        CurrentAlarmState = CurrentAlarmState is AlarmState.None ? AlarmState.Alarm : AlarmState.None;
                        AlarmStateChanged = true;
                    }

                    if (AlarmStateChanged)
                    {
                        switch (CurrentAlarmState)
                        {
                            case AlarmState.Alarm:
                                KeyHelpers.DisplayKeyHelp("AlarmSwitchKey", Settings.ToggleBankHeistAlarmSoundKey, Settings.ToggleBankHeistAlarmSoundModifierKey);
                                BankAlarm.PlayLooping();
                                break;
                            default:
                            case AlarmState.None:
                                BankAlarm.Stop();
                                break;
                        }
                        AlarmStateChanged = false;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private void MakeNearbyPedsFlee()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();

                foreach (var ped in World.GetEntities(CalloutPosition, 150f, GetEntitiesFlags.ConsiderAllPeds | GetEntitiesFlags.ExcludePlayerPed | GetEntitiesFlags.ExcludePoliceOfficers).Cast<Ped>())
                {
                    GameFiber.Yield();
                    if (CalloutEntities.Contains(ped)) continue;
                    if (ped is not null && ped.IsValid() && ped.Exists())
                    {
                        if (ped != Main.Player && !ped.CreatedByTheCallingPlugin)
                        {
                            if (ped.IsInAnyVehicle(false))
                            {
                                if (ped.CurrentVehicle.Exists())
                                {
                                    ped.CurrentVehicle.Delete();
                                }
                            }
                            ped.Delete();
                        }
                    }
                }
            }
        });
    }

    private void SneakyRobbersAI()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    foreach (var robber in AllSneakRobbers)
                    {
                        if (robber is not null && robber.IsValid() && robber.Exists() && robber.IsAlive)
                        {
                            if (!FightingSneakRobbers.Contains(robber))
                            {
                                var rsP = XmlManager.PacificBankHeistConfig.RobbersSneakPosition;
                                var index = AllSneakRobbers.IndexOf(robber);
                                var pos = new Vector3(rsP[index].X, rsP[index].Y, rsP[index].Z);
                                if (Vector3.Distance(robber.Position, pos) > 0.7f)
                                {
                                    robber.Tasks.FollowNavigationMeshToPosition(pos, rsP[index].Heading, 2f).WaitForCompletion(300);
                                }
                                else
                                {
                                    if (rsP[index].IsRight)
                                    {
                                        if (!NativeFunction.Natives.IS_ENTITY_PLAYING_ANIM<bool>(robber, SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 3))
                                        {
                                            robber.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                        }
                                    }
                                    else
                                    {
                                        if (!NativeFunction.Natives.IS_ENTITY_PLAYING_ANIM<bool>(robber, SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 3))
                                        {
                                            robber.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                        }
                                    }
                                }
                                var nearestPeds = robber.GetNearbyPeds(3);
                                if (nearestPeds.Length > 0)
                                {
                                    foreach (var nearestPed in nearestPeds)
                                    {
                                        if (nearestPed is not null && nearestPed.IsValid() && nearestPed.Exists() && nearestPed.IsAlive)
                                        {
                                            if (nearestPed.RelationshipGroup == Main.Player.RelationshipGroup || nearestPed.RelationshipGroup == RelationshipGroup.Cop)
                                            {
                                                if (Vector3.Distance(nearestPed.Position, robber.Position) < 3.9f)
                                                {
                                                    if (Math.Abs(nearestPed.Position.Z - robber.Position.Z) < 0.9f)
                                                    {
                                                        SneakyRobberFight(robber, nearestPed);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private Entity entityPlayerAimingAtSneakyRobber = null;
    private void SneakyRobberFight(Ped sneak, Ped nearestPed)
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                FightingSneakRobbers.Add(sneak);
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!nearestPed.Exists()) break;
                    if (!sneak.Exists() || !sneak.IsAlive) break;
                    if (!nearestPed.IsAlive) break;
                    if (Vector3.Distance(nearestPed.Position, sneak.Position) > 5.1f) break;
                    else if (Vector3.Distance(nearestPed.Position, sneak.Position) < 1.70f) break;
                    try
                    {
                        unsafe
                        {
                            uint entityHandle;
                            NativeFunction.Natives.x2975C866E6713290(Game.LocalPlayer, new IntPtr(&entityHandle)); // Stores the entity the player is aiming at in the uint provided in the second parameter.
                            entityPlayerAimingAtSneakyRobber = World.GetEntityByHandle<Entity>(entityHandle);
                        }
                    }
                    catch // (Exception e)
                    {
                        // Logger.Error(e.ToString());
                    }
                    if (entityPlayerAimingAtSneakyRobber == sneak) break;
                    if (RescuingHostage) break;
                }
                if (sneak is not null && sneak.IsValid() && sneak.Exists())
                {
                    sneak.Tasks.FightAgainstClosestHatedTarget(15f);
                    sneak.RelationshipGroup = RobbersRG;
                }
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!sneak.Exists()) break;
                    if (!nearestPed.Exists()) break;
                    NativeFunction.Natives.STOP_CURRENT_PLAYING_AMBIENT_SPEECH(sneak);
                    if (nearestPed.IsDead)
                    {
                        foreach (var hostage in SpawnedHostages)
                        {
                            if (Math.Abs(hostage.Position.Z - sneak.Position.Z) < 0.6f)
                            {
                                if (Vector3.Distance(hostage.Position, sneak.Position) < 14f)
                                {
                                    int waitCount = 0;
                                    while (hostage.IsAlive)
                                    {
                                        GameFiber.Yield();
                                        waitCount++;
                                        if (waitCount > 450)
                                        {
                                            hostage.Kill();
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    if (sneak.IsDead) break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            finally
            {
                FightingSneakRobbers.Remove(sneak);
            }
        });
    }

    private void HandleHostages()
    {
        Game.FrameRender += TimerBarsProcess;
        GameFiber.StartNew(() =>
        {
            int waitCountForceAttack = 0;
            int enterAmbulanceCount = 0;
            int deleteSafeHostageCount = 0;
            int subtitleCount = 0;
            Ped closeHostage = null;
            while (IsCalloutRunning)
            {
                try
                {
                    waitCountForceAttack++;
                    enterAmbulanceCount++;

                    GameFiber.Yield();
                    if (waitCountForceAttack > 250)
                    {
                        waitCountForceAttack = 0;
                    }
                    if (enterAmbulanceCount > 101)
                    {
                        enterAmbulanceCount = 101;
                    }
                    foreach (Ped hostage in SpawnedHostages)
                    {
                        GameFiber.Yield();
                        if (hostage.Exists())
                        {
                            if (hostage.IsAlive)
                            {
                                if (Functions.IsPedGettingArrested(hostage) || Functions.IsPedArrested(hostage))
                                {
                                    SpawnedHostages[SpawnedHostages.IndexOf(hostage)] = hostage.ClonePed();
                                }
                                hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                                if (!Main.Player.IsShooting)
                                {
                                    if (Vector3.Distance(hostage.Position, Main.Player.Position) < 1.45f)
                                    {
                                        if (KeyHelpers.IsKeysDownRightNow(Settings.HostageRescueKey, Settings.HostageRescueModifierKey))
                                        {
                                            var direction = hostage.Position - Main.Player.Position;
                                            direction.Normalize();
                                            RescuingHostage = true;
                                            Main.Player.Tasks.AchieveHeading(MathHelper.ConvertDirectionToHeading(direction)).WaitForCompletion(1200);
                                            hostage.RelationshipGroup = "COP";
                                            Conversations.Talk([(Settings.OfficerName, Localization.GetString("RescueHostage"))], false);
                                            Main.Player.Tasks.PlayAnimation("random@rescue_hostage", "bystander_helping_girl_loop", 1.5f, AnimationFlags.None).WaitForCompletion(3000);

                                            if (hostage.IsAlive)
                                            {
                                                hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_get_up", 0.9f, AnimationFlags.None).WaitForCompletion(6000);
                                                Main.Player.Tasks.ClearImmediately();
                                                if (hostage.IsAlive)
                                                {
                                                    var data = XmlManager.PacificBankHeistConfig.HostageSafePosition;
                                                    var pos = new Vector3(data.X, data.Y, data.Z);
                                                    hostage.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.55f);
                                                    RescuedHostages.Add(hostage);
                                                    SpawnedHostages.Remove(hostage);
                                                }
                                                else
                                                {
                                                    Main.Player.Tasks.ClearImmediately();
                                                }
                                            }
                                            else
                                            {
                                                Main.Player.Tasks.ClearImmediately();
                                            }
                                            RescuingHostage = false;
                                        }
                                        else
                                        {
                                            subtitleCount++;
                                            closeHostage = hostage;
                                            if (subtitleCount > 5)
                                            {
                                                KeyHelpers.DisplayKeyHelp("BankHeistReleaseHostage", Settings.HostageRescueKey, Settings.HostageRescueModifierKey);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (hostage == closeHostage)
                                        {
                                            subtitleCount = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SpawnedHostages.Remove(hostage);
                                AliveHostagesCount--;
                            }
                        }
                        else
                        {
                            SpawnedHostages.Remove(hostage);
                            AliveHostagesCount--;
                        }
                    }
                    foreach (var rescued in RescuedHostages)
                    {
                        if (rescued.Exists() && rescued.IsAlive)
                        {
                            if (SpawnedHostages.Contains(rescued))
                            {
                                SpawnedHostages.Remove(rescued);
                            }
                            var data = XmlManager.PacificBankHeistConfig.HostageSafePosition;
                            var pos = new Vector3(data.X, data.Y, data.Z);
                            if (Vector3.Distance(rescued.Position, pos) < 3f)
                            {
                                SafeHostages.Add(rescued);
                                SafeHostagesCount++;
                            }
                            if (Functions.IsPedGettingArrested(rescued) || Functions.IsPedArrested(rescued))
                            {
                                RescuedHostages[RescuedHostages.IndexOf(rescued)] = rescued.ClonePed();
                            }
                            rescued.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.55f).WaitForCompletion(200);

                            if (waitCountForceAttack > 150)
                            {
                                var nearest = rescued.GetNearbyPeds(2)[0];
                                if (nearest == Main.Player)
                                {
                                    nearest = rescued.GetNearbyPeds(2)[1];
                                }
                                if (AllRobbers.Contains(nearest))
                                {
                                    nearest.Tasks.FightAgainst(rescued);
                                    waitCountForceAttack = 0;
                                }
                            }
                        }
                        else
                        {
                            RescuedHostages.Remove(rescued);
                            AliveHostagesCount--;
                        }
                    }
                    foreach (var safe in SafeHostages)
                    {
                        if (safe.Exists())
                        {
                            if (RescuedHostages.Contains(safe))
                            {
                                RescuedHostages.Remove(safe);
                            }
                            safe.IsInvincible = true;
                            if (!safe.IsInAnyVehicle(true))
                            {
                                if (enterAmbulanceCount > 100)
                                {
                                    if (AllAmbulance[1].IsSeatFree(2))
                                    {
                                        safe.Tasks.EnterVehicle(AllAmbulance[1], 2);
                                    }
                                    else if (AllAmbulance[1].IsSeatFree(1))
                                    {
                                        safe.Tasks.EnterVehicle(AllAmbulance[1], 1);
                                    }
                                    else
                                    {
                                        AllAmbulance[1].GetPedOnSeat(2).Delete();
                                        safe.Tasks.EnterVehicle(AllAmbulance[1], 2);
                                    }
                                    enterAmbulanceCount = 0;
                                }
                            }
                            else
                            {
                                deleteSafeHostageCount++;
                                if (deleteSafeHostageCount > 50)
                                {
                                    if (Vector3.Distance(Main.Player.Position, safe.Position) > 22f)
                                    {
                                        if (safe.IsInAnyVehicle(false))
                                        {
                                            safe.Delete();
                                            deleteSafeHostageCount = 0;
                                            NativeFunction.Natives.SET_VEHICLE_DOORS_SHUT(AllAmbulance[1], true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            SafeHostages.Remove(safe);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        });
    }

    private void TimerBarsProcess(object sender, GraphicsEventArgs e)
    {
        if (IsFighting || (SurrenderComplete && TalkedToCommander2nd))
        {
            if (TBPool is not null)
            {
                RescuedHostagesTB.Text = $"{SafeHostagesCount.ToString()}/{TotalHostagesCount.ToString()}";
                DiedHostagesTB.Text = $"{(TotalHostagesCount - AliveHostagesCount).ToString()}";
                DiedHostagesTB.Highlight = TotalHostagesCount - AliveHostagesCount is > 0 ? HudColor.Red.GetColor() : HudColor.Green.GetColor();
                TBPool.Draw();
            }
        }
        if (!IsCalloutRunning || DoneFighting)
        {
            Game.FrameRender -= TimerBarsProcess;
        }
    }

    private void HandleOpenBackRiotVan()
    {
        GameFiber.StartNew(() =>
        {
            int CoolDown = 0;
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    if (CoolDown > 0) CoolDown--;

                    foreach (var riot in AllRiot)
                    {
                        if (riot is not null && riot.IsValid() && riot.Exists())
                        {
                            if (Vector3.Distance(riot.GetOffsetPosition(Vector3.RelativeBack * 4f), Main.Player.Position) < 2f)
                            {
                                if (KeyHelpers.IsKeysDownRightNow(Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey))
                                {
                                    if (CoolDown > 0)
                                    {
                                        HudHelpers.DisplayNotification(Localization.GetString("GearRunOut"));
                                    }
                                    else
                                    {
                                        CoolDown = 10000;
                                        Main.Player.Tasks.EnterVehicle(riot, 1).WaitForCompletion();
                                        Main.Player.Armor = 100;
                                        Main.Player.Health = Main.Player.MaxHealth;
                                        Main.Player.GiveWeapon([.. XmlManager.PacificBankHeistConfig.WeaponInRiot], false);
                                        NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "PURCHASE", "HUD_LIQUOR_STORE_SOUNDSET", 1);
                                        Main.Player.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion();
                                    }
                                }
                                else
                                {
                                    if (CoolDown is 0)
                                    {
                                        KeyHelpers.DisplayKeyHelp("EnterRiot", [$"~{RIOT_BLIP.GetIconToken()}~"], Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey, sound: false);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private void ToggleMobilePhone(Ped ped, bool toggle)
    {
        if (toggle)
        {
            NativeFunction.Natives.SET_PED_CAN_SWITCH_WEAPON(ped, false);
            ped.Inventory.GiveNewWeapon(new WeaponAsset("WEAPON_UNARMED"), -1, true);
            MobilePhone = new(PhoneModel, new(0, 0, 0));
            int boneIndex = NativeFunction.Natives.GET_PED_BONE_INDEX<int>(ped, (int)PedBoneId.RightPhHand);
            NativeFunction.Natives.ATTACH_ENTITY_TO_ENTITY(MobilePhone, ped, boneIndex, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, 1);
            ped.Tasks.PlayAnimation("cellphone@", "cellphone_call_listen_base", 1.3f, AnimationFlags.Loop | AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask);
        }
        else
        {
            NativeFunction.Natives.SET_PED_CAN_SWITCH_WEAPON(ped, true);
            ped.Tasks.Clear();
            if (GameFiber.CanSleepNow)
            {
                GameFiber.Wait(800);
            }
            if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists()) MobilePhone.Delete();
        }
    }

    private void GetWife()
    {
        Main.Player.IsPositionFrozen = true;
        var vData = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceCruisers]);
        var data = XmlManager.PacificBankHeistConfig.WifePosition;
        WifeCar = new(vData.Model, new(data.X, data.Y, data.Z), data.Heading)
        {
            IsPersistent = true,
            IsSirenOn = true
        };
        if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists())
        {
            WifeDriver = WifeCar.CreateRandomDriver();
            if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists())
            {
                WifeDriver.IsPersistent = true;
                WifeDriver.BlockPermanentEvents = true;
                var wData = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.WifeModels]);
                Wife = new Ped(wData.Model, Vector3.Zero, 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                };
                if (Wife is not null && Wife.IsValid() && Wife.Exists())
                {
                    Wife.WarpIntoVehicle(WifeCar, 1);
                    CalloutEntities.Add(Wife);
                    CalloutEntities.Add(WifeDriver);
                    CalloutEntities.Add(WifeCar);

                    var destination = new Vector3(XmlManager.PacificBankHeistConfig.WifeVehicleDestination.X, XmlManager.PacificBankHeistConfig.WifeVehicleDestination.Y, XmlManager.PacificBankHeistConfig.WifeVehicleDestination.Z);
                    WifeDriver.Tasks.DriveToPosition(destination, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                    while (true)
                    {
                        GameFiber.Yield();
                        if (Vector3.Distance(WifeCar.Position, destination) < 6f) break;
                    }
                    Wife.Tasks.LeaveVehicle(LeaveVehicleFlags.None);
                    Wife.Tasks.FollowNavigationMeshToPosition(Main.Player.GetOffsetPosition(Vector3.RelativeRight * 1.5f), Main.Player.Heading, 1.9f).WaitForCompletion(60000);
                    Main.Player.IsPositionFrozen = false;
                }
            }
        }
    }

    private void ClearUnrelatedEntities()
    {
        foreach (var ped in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
        {
            GameFiber.Yield();
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                if (ped != Main.Player && !ped.CreatedByTheCallingPlugin)
                {
                    if (!CalloutEntities.Contains(ped))
                    {
                        if (Vector3.Distance(ped.Position, CalloutPosition) < 50f)
                        {
                            ped.Delete();
                        }
                    }
                }
            }
        }
        foreach (var vehicle in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderGroundVehicles).Cast<Vehicle>())
        {
            GameFiber.Yield();
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                if (vehicle != Main.Player.CurrentVehicle && !vehicle.CreatedByTheCallingPlugin)
                {
                    if (!CalloutEntities.Contains(vehicle))
                    {
                        if (Vector3.Distance(vehicle.Position, CalloutPosition) < 50f)
                        {
                            vehicle.Delete();
                        }
                    }
                }
            }
        }
    }
}