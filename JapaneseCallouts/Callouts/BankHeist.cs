namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] BankHeist", CalloutProbability.Low)]
internal class BankHeist : CalloutBase
{
    private const string ALARM_SOUND_FILE_NAME = "BankHeistAlarm.wav";

    private static readonly SoundPlayer BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{ALARM_SOUND_FILE_NAME}");
    private const ulong _DOOR_CONTROL = 0x9b12f9a24fabedb0;

    private Vector3 BankLocation = new(250.9f, 219.0f, 106.2f);

    // Vehicles
    private readonly (Vector3 pos, float heading)[] PoliceCruiserPositions =
    [
        (new(271.3f, 180.6f, 104.3f), 69.4f),
        (new(258.9f, 185.2f, 104.4f), 69.8f),
        (new(246.8f, 189.8f, 104.8f), 68.1f),
        (new(236.9f, 193.9f, 104.9f), -110.7f),
        (new(227.7f, 197.9f, 105.0f), 64.5f),
        (new(220.6f, 217.9f, 105.1f), -18.9f),
        (new(223.9f, 226.8f, 105.1f), -20.0f),
        (new(231.3f, 173.6f, 104.9f), -110.2f)
    ];
    private readonly (Vector3 pos, float heading)[] PoliceTransportPositions =
    [
        (new(273.9f, 191.7f, 104.6f), 54.3f),
        (new(219.0f, 175.1f, 105.2f), -111.1f)
    ];
    private readonly (Vector3 pos, float heading)[] RiotPositions =
    [
        (new(220.2f, 209.2f, 105.1f), 23.2f),
        (new(236.9f, 205.9f, 104.9f), 71.1f),
        (new(265.4f, 192.2f, 104.4f), -139.9f)
    ];
    private readonly (Vector3 pos, float heading)[] AmbulancePositions =
    [
        (new(263.2f, 158.9f, 104.2f), -110.3f),
        (new(254.0f, 162.2f, 104.4f), -109.2f)
    ];
    private readonly (Vector3 pos, float heading)[] FiretruckPositions =
    [
        (new(239.6f, 170.7f, 105.1f), -109.9f)
    ];
    // Barrier
    private readonly (Vector3 pos, float heading)[] BarrierPositions =
    [
        (new(266.5f, 182.4f, 103.6f), -20.1f),
        (new(263.4f, 183.6f, 103.7f), -20.1f),
        (new(254.3f, 186.8f, 103.9f), -20.1f),
        (new(251.2f, 187.9f, 103.9f), -20.1f),
        (new(241.6f, 192.0f, 104.1f), -21.5f),
        (new(232.2f, 195.8f, 104.2f), -22.2f),
        (new(223.8f, 200.1f, 104.4f), -33.5f),
        (new(220.9f, 203.0f, 104.4f), -53.1f),
        (new(222.2f, 222.2f, 104.4f), -110.2f),
        (new(227.7f, 228.2f, 104.5f), 159.8f),
        (new(230.5f, 227.2f, 104.5f), 159.8f)
    ];
    // Officers
    private readonly (Vector3 pos, float heading)[] AimingOfficerPositions =
    [
        (new(219.8f, 220.3f, 105.5f), -120.1f),
        (new(218.5f, 216.5f, 105.5f), -105.7f),
        (new(226.1f, 197.0f, 105.4f), -19.9f),
        (new(234.4f, 193.3f, 105.2f), -21.4f),
        (new(247.8f, 187.9f, 105.0f), -29.9f),
        (new(257.3f, 184.2f, 104.9f), 2.6f),
        (new(260.5f, 183.1f, 104.8f), -12.2f)
    ];
    private readonly (Vector3 pos, float heading)[] StandingOfficerPositions =
    [
        (new(215.2f, 177.1f, 105.3f), 70.4f),
        (new(218.6f, 207.0f, 105.4f), 113.0f)
    ];
    // EMS
    private readonly (Vector3 pos, float heading)[] FirefighterPositions =
    [
        (new(244.9f, 168.3f, 104.9f), -24.0f)
    ];
    private readonly (Vector3 pos, float heading)[] ParamedicPositions =
    [
        (new(250.8f, 165.5f, 104.7f), -21.2f),
        (new(259.8f, 162.4f, 104.6f), -20.5f)
    ];
    // SWAT
    private readonly (Vector3 pos, float heading)[] LeftSittingSWATPositions =
    [
        (new(231.6f, 211.8f, 105.4f), 84.4f),
        (new(232.0f, 211.0f, 105.4f), 93.8f),
        (new(232.9f, 210.4f, 105.4f), 129.7f),
        (new(260.7f, 200.2f, 104.9f), 133.2f),
        (new(261.5f, 199.9f, 104.9f), 118.9f),
        (new(262.3f, 199.6f, 104.9f), 127.6f)
    ];
    private readonly (Vector3 pos, float heading)[] RightSittingSWATPositions =
    [
        (new(228.7f, 218.0f, 105.5f), 150.2f),
        (new(228.9f, 219.2f, 105.5f), 125.3f),
        (new(255.7f, 202.1f, 105.0f), -128.7f),
        (new(254.8f, 202.5f, 105.0f), -145.8f)
    ];
    private readonly (Vector3 pos, float heading)[] RightLookingSWATPositions =
    [
        (new(229.1f, 217.1f, 105.5f), 171.3f),
        (new(256.6f, 201.8f, 105.0f), -138.0f)
    ];
    // Bank Door
    private readonly Vector3[] BankDoorPositions =
    [
        new Vector3(231.5f, 215.2f, 106.2f),
        new Vector3(259.1f, 202.7f, 106.2f)
    ];
    // Captain
    private readonly (Vector3 pos, float heading) CaptainPosition = (new(271.9f, 190.8f, 104.7f), 138.2f);
    // Hostage
    private readonly List<Vector3> HostagePositions =
    [
        new(242.8f, 228.3f, 106.2f),
        new(248.3f, 229.7f, 106.2f),
        new(241.4f, 221.5f, 106.2f),
        new(245.6f, 216.6f, 106.2f),
        new(253.6f, 218.9f, 106.2f),
        new(267.2f, 223.0f, 110.2f),
        new(262.3f, 224.8f, 101.6f),
        new(262.6f, 208.2f, 110.2f),
        new(250.6f, 209.1f, 110.2f),
        new(243.3f, 211.7f, 110.2f),
        new(236.1f, 216.3f, 110.2f),
        new(255.0f, 224.7f, 106.2f)
    ];
    // Vehicle Model
    private readonly Model[] PoliceCruiserModels = ["POLICE", "POLICE2", "POLICE3", "POLICE4"];
    private readonly Model[] PoliceTransportModels = ["POLICET"];
    private readonly Model[] RiotModels = ["RIOT"];
    private readonly Model[] AmbulanceModels = ["AMBULANCE"];
    private readonly Model[] FiretruckModels = ["FIRETRUK"];
    private readonly Model BarrierModel = "PROP_BARRIER_WORK05";
    private readonly Model InvisibleWallModel = "P_ICE_BOX_01_S";
    private readonly Model[] CopModels = ["S_M_Y_COP_01", "S_F_Y_COP_01"];
    private readonly Model SwatModel = "S_M_Y_SWAT_01";
    private readonly Model ParamedicModel = "S_M_M_PARAMEDIC_01";
    private readonly Model FirefighterModel = "S_M_Y_FIREMAN_01";
    private readonly Model CaptainModel = "IG_FBISUIT_01";
    private readonly Model[] HostageModels = ["A_M_M_BUSINESS_01", "A_M_Y_BUSINESS_01", "A_M_Y_BUSINESS_02", "A_M_Y_BUSINESS_03", "A_F_Y_BUSINESS_01", "A_F_Y_FEMALEAGENT"];
    private readonly WeaponHash[] OfficerWeapons = [WeaponHash.Pistol];
    private readonly WeaponHash[] SWATWeapons = [WeaponHash.CarbineRifle, WeaponHash.AssaultRifle];

    private const string SWAT_ANIMATION_DICTIONARY = "cover@weapon@rpg";
    private const string SWAT_ANIMATION_LEFT = "blindfire_low_l_corner_exit";
    private const string SWAT_ANIMATION_RIGHT_LOOKING = "blindfire_low_r_corner_exit";
    private const string SWAT_ANIMATION_RIGHT = "blindfire_low_r_centre_exit";
    private const string HOSTAGE_ANIMATION_DICTIONARY = "random@arrests";
    private const string HOSTAGE_ANIMATION_KNEELING = "kneeling_arrest_idle";

    private readonly List<Vehicle> AllPoliceVehicles = [];
    private readonly List<Vehicle> AllRiot = [];
    private readonly List<Vehicle> AllAmbulance = [];
    private readonly List<Vehicle> AllFiretruck = [];
    private readonly List<Ped> AllOfficers = [];
    private readonly List<Ped> AllSWAT = [];
    private readonly List<Ped> AllEMS = [];
    private readonly List<Ped> AllRobbers = [];
    private readonly List<Ped> AllHostages = [];
    private readonly List<Ped> SpawnedHostages = [];
    private readonly List<Object> AllBarriers = [];
    private readonly List<Ped> AllBarrierPeds = [];
    private readonly List<Object> AllInvisibleWalls = [];

    private readonly List<Entity> CalloutEntities = [];

    private Blip BankBlip;
    private Ped Captain;
    private Blip CaptainBlip;

    private int HostageCount = 0;
    private int AliveHostageCount = 0;
    private int DiedHostageCount = 0;
    private int RemainingRobberCount = 0;
    private int DiedRobberCount = 0;

    private bool EnableAlarmSound = true;
    private bool TalkedToCaptain = false;
    private bool IsFighting = false;

    internal override void Setup()
    {
        CalloutPosition = BankLocation;
        CalloutMessage = CalloutsName.BankHeist;
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 30f);
        Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99", CalloutPosition);

        if (Main.IsCalloutInterfaceAPIExist)
        {
            CalloutInterfaceAPIFunctions.SendMessage(this, CalloutsDescription.BankHeist);
        }
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        BankAlarm.Load();
        if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
        {
            CalloutEntities.Add(Game.LocalPlayer.Character.CurrentVehicle);
        }
        HudExtensions.DisplayNotification(CalloutsDescription.BankHeist);
        CalloutHandler();
    }

    internal override void Update()
    {
    }

    internal override void EndCallout(bool notAccepted = false, bool isPlayerDead = false)
    {
    }

    private void CalloutHandler()
    {
        BankBlip = new(CalloutPosition, 20f)
        {
            Color = Color.Yellow,
            RouteColor = Color.Yellow,
            IsRouteEnabled = true
        };
        GameFiber.StartNew(() =>
        {
            while (Vector3.Distance(Game.LocalPlayer.Character.Position, CalloutPosition) > 350f)
            {
                GameFiber.Yield();
                GameFiber.Wait(1000);
            }
            if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
            {
                CalloutEntities.Add(Game.LocalPlayer.Character.CurrentVehicle);
                var passengers = Game.LocalPlayer.Character.CurrentVehicle.Passengers;
                if (passengers.Length > 0)
                {
                    foreach (var passenger in passengers)
                    {
                        CalloutEntities.Add(passenger);
                    }
                }
            }
            GameFiber.Yield();
            ClearUnrelatedEntities();
            GameFiber.Yield();
            SpawnVehicles();
            GameFiber.Yield();
            PlaceBarrier();
            GameFiber.Yield();
            SpawnOfficers();
            GameFiber.Yield();
            SpawnEMS();
            GameFiber.Yield();
            SpawnHostages();

            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                Game.LocalPlayer.Character.CanAttackFriendlies = false;
                Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, "ROBBERS", Relationship.Hate);
                Game.SetRelationshipBetweenRelationshipGroups("ROBBERS", RelationshipGroup.Cop, Relationship.Hate);
                Game.SetRelationshipBetweenRelationshipGroups("ROBBERS", Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);
                Game.SetRelationshipBetweenRelationshipGroups(Game.LocalPlayer.Character.RelationshipGroup, "ROBBERS", Relationship.Hate);
                Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
                Game.SetRelationshipBetweenRelationshipGroups(Game.LocalPlayer.Character.RelationshipGroup, RelationshipGroup.Cop, Relationship.Respect);
                Game.SetRelationshipBetweenRelationshipGroups("HOSTAGE", Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
                Game.SetRelationshipBetweenRelationshipGroups("SNEAKYROBBERS", Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);
                Game.LocalPlayer.Character.IsInvincible = false;
                NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 0.45f);
                NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 0.92f);
                NativeFunction.Natives.SET_AI_MELEE_WEAPON_DAMAGE_MODIFIER(1f);
                CallByHash<uint>(_DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                CallByHash<uint>(_DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                CallByHash<uint>(_DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);

                //When player has just arrived
                if (!TalkedToCaptain && !IsFighting)
                {
                    if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                    {
                        if (Vector3.Distance(Game.LocalPlayer.Character.Position, Captain.Position) < 4f)
                        {
                            if (Settings.SpeakWithThePersonModifierKey is Keys.None)
                            {
                                HudExtensions.DisplayNotification(string.Format(General.PressToTalkWith, Settings.SpeakWithThePersonKey.GetInstructionalId(), CalloutsText.Captain));
                            }
                            else
                            {
                                HudExtensions.DisplayNotification(string.Format(General.PressToTalkWith, $"{Settings.SpeakWithThePersonKey.GetInstructionalId()} ~+~ {Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}", CalloutsText.Captain));
                            }
                            if (KeyExtensions.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                            {
                                TalkedToCaptain = true;
                                DetermineInitialDialogue();
                            }
                        }
                        else
                        {
                            Game.DisplayHelp("~h~Officer, please report to ~g~Captain Wells ~s~for briefing.");
                        }
                    }
                }
            }
        });
    }

    private void SpawnVehicles()
    {
        foreach (var (pos, heading) in PoliceCruiserPositions)
        {
            var vehicle = new Vehicle(PoliceCruiserModels[Main.MersenneTwister.Next(PoliceCruiserModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllPoliceVehicles.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in PoliceTransportPositions)
        {
            var vehicle = new Vehicle(PoliceTransportModels[Main.MersenneTwister.Next(PoliceTransportModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllPoliceVehicles.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in RiotPositions)
        {
            var vehicle = new Vehicle(RiotModels[Main.MersenneTwister.Next(RiotModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllPoliceVehicles.Add(vehicle);
            AllRiot.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in AmbulancePositions)
        {
            var vehicle = new Vehicle(AmbulanceModels[Main.MersenneTwister.Next(AmbulanceModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllAmbulance.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in FiretruckPositions)
        {
            var vehicle = new Vehicle(FiretruckModels[Main.MersenneTwister.Next(FiretruckModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllFiretruck.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
    }

    private void PlaceBarrier()
    {
        foreach (var (pos, heading) in BarrierPositions)
        {
            var barrier = new Object(BarrierModel, pos, heading)
            {
                IsPositionFrozen = true,
                IsPersistent = true
            };
            var invisibleWall = new Object(InvisibleWallModel, barrier.Position, heading)
            {
                IsVisible = false,
                IsPersistent = true
            };
            var barrierPed = new Ped(invisibleWall.Position)
            {
                IsVisible = false,
                IsPositionFrozen = true,
                BlockPermanentEvents = true,
                IsPersistent = true
            };
            AllBarriers.Add(barrier);
            AllInvisibleWalls.Add(invisibleWall);
            AllBarrierPeds.Add(barrierPed);
        }
    }

    private void SpawnOfficers()
    {
        foreach (var (pos, heading) in LeftSittingSWATPositions)
        {
            var swat = new Ped(SwatModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(swat);
            Functions.SetCopAsBusy(swat, true);
            swat.Inventory.GiveNewWeapon(SWATWeapons[Main.MersenneTwister.Next(SWATWeapons.Length - 1)], 10000, true);
            swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);

            AllSWAT.Add(swat);
            CalloutEntities.Add(swat);
        }
        foreach (var (pos, heading) in RightLookingSWATPositions)
        {
            var swat = new Ped(SwatModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(swat);
            Functions.SetCopAsBusy(swat, true);
            swat.Inventory.GiveNewWeapon(SWATWeapons[Main.MersenneTwister.Next(SWATWeapons.Length - 1)], 10000, true);
            swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT_LOOKING, 1f, AnimationFlags.StayInEndFrame);
            AllSWAT.Add(swat);
            CalloutEntities.Add(swat);
        }
        foreach (var (pos, heading) in RightSittingSWATPositions)
        {
            var swat = new Ped(SwatModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(swat);
            Functions.SetCopAsBusy(swat, true);
            swat.Inventory.GiveNewWeapon(SWATWeapons[Main.MersenneTwister.Next(SWATWeapons.Length - 1)], 10000, true);
            swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 1f, AnimationFlags.StayInEndFrame);
            AllSWAT.Add(swat);
            CalloutEntities.Add(swat);
        }
        foreach (var (pos, heading) in AimingOfficerPositions)
        {
            var officer = new Ped(CopModels[Main.MersenneTwister.Next(CopModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(officer);
            Functions.SetCopAsBusy(officer, true);
            officer.Inventory.GiveNewWeapon(OfficerWeapons[Main.MersenneTwister.Next(OfficerWeapons.Length - 1)], 10000, true);
            var AimPoint = Vector3.Distance(officer.Position, BankDoorPositions[0]) < Vector3.Distance(officer.Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
            NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(officer, AimPoint.X, AimPoint.Y, AimPoint.Z, -1, false, false);
            AllSWAT.Add(officer);
            CalloutEntities.Add(officer);
        }
        foreach (var (pos, heading) in StandingOfficerPositions)
        {
            var officer = new Ped(CopModels[Main.MersenneTwister.Next(CopModels.Length - 1)], pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(officer);
            Functions.SetCopAsBusy(officer, true);
            officer.Inventory.GiveNewWeapon(OfficerWeapons[Main.MersenneTwister.Next(OfficerWeapons.Length - 1)], 10000, true);
            AllSWAT.Add(officer);
            CalloutEntities.Add(officer);
        }
        Captain = new Ped(CaptainModel, CaptainPosition.pos, CaptainPosition.heading)
        {
            BlockPermanentEvents = true,
            IsPersistent = true,
            IsInvincible = true,
            RelationshipGroup = RelationshipGroup.Cop
        };
        Functions.SetPedCantBeArrestedByPlayer(Captain, true);

        CaptainBlip = Captain.AttachBlip();
        CaptainBlip.Color = Color.Green;
        CalloutEntities.Add(Captain);
    }

    private void SpawnEMS()
    {
        foreach (var (pos, heading) in ParamedicPositions)
        {
            var paramedic = new Ped(ParamedicModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                Health = 200
            };
            AllEMS.Add(paramedic);
            CalloutEntities.Add(paramedic);
        }
        foreach (var (pos, heading) in FirefighterPositions)
        {
            var firefighter = new Ped(FirefighterModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                Armor = 200
            };
            AllEMS.Add(firefighter);
            CalloutEntities.Add(firefighter);
        }
    }

    private void SpawnHostages()
    {
        for (int i = 0; i < 8; i++)
        {
            var hostage = new Ped(new Model(HostageModels[Main.MersenneTwister.Next(HostageModels.Length - 1)]), HostagePositions[Main.MersenneTwister.Next(HostagePositions.Count - 1)], Main.MersenneTwister.Next(0, 360))
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = "HOSTAGE",
                CanAttackFriendlies = false,
                Armor = 0,
                Health = 100,
            };
            NativeFunction.Natives.SET_PED_CAN_RAGDOLL(hostage, false);
            AllHostages.Add(hostage);
            SpawnedHostages.Add(hostage);
            CalloutEntities.Add(hostage);
            hostage.Tasks.PlayAnimation(HOSTAGE_ANIMATION_DICTIONARY, HOSTAGE_ANIMATION_KNEELING, 1f, AnimationFlags.Loop);
            GameFiber.Yield();
            AliveHostageCount++;
            HostageCount++;
            HostagePositions.RemoveAt(i);
        }
    }

    private void ClearUnrelatedEntities()
    {
        foreach (var ped in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
        {
            GameFiber.Yield();
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                if (ped != Game.LocalPlayer.Character)
                {
                    if (!ped.CreatedByTheCallingPlugin)
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
        }
        foreach (var vehicle in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderGroundVehicles).Cast<Vehicle>())
        {
            GameFiber.Yield();
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                if (vehicle != Game.LocalPlayer.Character.CurrentVehicle)
                {
                    if (!vehicle.CreatedByTheCallingPlugin)
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
}