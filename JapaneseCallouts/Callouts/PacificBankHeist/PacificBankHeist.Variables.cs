namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal partial class PacificBankHeist
{
    internal EPacificBankHeistStatus Status = EPacificBankHeistStatus.Init;

    internal bool IsNegotiationSucceed = false;
    internal bool IsAlarmEnabled = true;
    internal bool IsCalloutFinished = false;
    internal bool SurrenderComplete = false;
    internal bool IsRescuingHostage = false;
    internal bool IsSWATFollowing = false;
    internal bool TalkedToCommander2nd = false;

    internal AlarmState CurrentAlarmState = AlarmState.None;
    internal SoundPlayer BankAlarm;
    internal Blip BankBlip, SideDoorBlip, CommanderBlip;
    internal Ped Commander, Wife, WifeDriver;
    internal Vehicle WifeCar;
    internal RObject MobilePhone;
    internal readonly TimerBarPool TBPool = [];
    internal TextTimerBar RescuedHostagesTB, DiedHostagesTB;
    internal readonly int HostageCount = Configuration.HostageCount > Configuration.HostagePositions.Length ? Configuration.HostagePositions.Length : Configuration.HostageCount;
    internal int AliveHostagesCount = 0, SafeHostagesCount = 0, TotalHostagesCount = 0, DiedRobbersCount = 0, FightingPacksUsed = 0;
    internal uint SpeedZoneId;

    internal readonly List<Vehicle> AllPoliceVehicles = [];
    internal readonly List<Vehicle> AllRiot = [];
    internal readonly List<Vehicle> AllAmbulance = [];
    internal readonly List<Ped> AllOfficers = [];
    internal readonly List<Ped> AllStandingOfficers = [];
    internal readonly List<Ped> AllAimingOfficers = [];
    internal readonly List<Ped> OfficersArresting = [];
    internal readonly List<Ped> OfficersTargetsToShoot = [];
    internal readonly List<Ped> AllSWATUnits = [];
    internal readonly List<Ped> AllRobbers = [];
    internal readonly List<Ped> AllSneakRobbers = [];
    internal readonly List<Ped> AllVaultRobbers = [];
    internal readonly List<Entity> AllBarriersEntities = [];
    internal readonly List<Ped> FightingSneakRobbers = [];
    internal readonly List<Ped> RescuedHostages = [];
    internal readonly List<Ped> SafeHostages = [];
    internal readonly List<Ped> AllHostages = [];
    internal readonly List<Ped> SpawnedHostages = [];
    internal readonly List<Blip> RiotBlips = [];
    internal readonly List<BlipPlus> EnemyBlips = [];
}