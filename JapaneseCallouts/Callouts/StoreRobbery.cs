// TODO: 最寄りのコンビニで強盗が起きるように調整

namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Store Robbery", CalloutProbability.VeryHigh)]
internal class StoreRobbery : CalloutBase
{
    private int index = 0, seconds = 80;
    private readonly List<Ped> robbers = [];
    private readonly Model RobbersModel = "MP_G_M_PROS_01";
    private readonly RelationshipGroup RobbersRG = "ROBBERS";
    private bool arrived = false;
    private LHandle pursuit;
    private Blip blip;
    private readonly Timer timer = new(1000);
    private readonly TimerBarPool tbPool = [];
    private readonly TextTimerBar timerBar = new(Localization.GetString("Remaining"), "");
    private readonly (Vector3 pos, (Vector3 pos, WeaponHash weapon, short ammo)[] robbersPos)[] robbersData =
    [
        (new(-49.0f, -1755.6f, 23.4f),
            [
                (new(-42.9f, -1749.3f, 29.4f), WeaponHash.Pistol, 5000),
                (new(-42.6f, -1753.8f, 29.4f), WeaponHash.Pistol, 5000),
                (new(-53.3f, -1748.8f, 29.4f), WeaponHash.Knife, 1),
            ]
        ),
        (new(29.3f, -1345.7f, 29.4f),
            [
                (new(29.3f, -1339.9f, 29.5f), WeaponHash.Pistol, 5000),
                (new(33.8f, -1343.3f, 29.5f), WeaponHash.Knife, 1),
                (new(25.0f, -1343.2f, 29.5f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(-711.4f, -913.1f, 19.2f),
            [
                (new(-708.9f, -904.6f, 19.2f), WeaponHash.Pistol, 5000),
                (new(-717.2f, -909.8f, 19.2f), WeaponHash.Knife, 1),
                (new(-706.2f, -909.8f, 19.2f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(-1224.5f, -905.0f, 12.3f),
            [
                (new(-1225.2f, -907.6f, 12.3f), WeaponHash.Pistol, 5000),
                (new(-1220.9f, -912.0f, 12.3f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(-1824.6f, 790.8f, 138.1f),
            [
                (new(-1828.0f, 799.0f, 138.1f), WeaponHash.Pistol, 5000),
                (new(-1822.9f, 797.0f, 138.1f), WeaponHash.Pistol, 5000),
                (new(-1830.4f, 789.4f, 138.3f), WeaponHash.Knife, 1),
            ]
        ),
        (new(-2969.9f, 390.5f, 15.0f),
            [
                (new(-2967.5f, 388.0f, 15.0f), WeaponHash.Pistol, 5000),
                (new(-2962.9f, 389.5f, 15.0f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(-3042.0f, 589.2f, 7.9f),
            [
                (new(-3047.9f, 587.7f, 7.9f), WeaponHash.Pistol, 5000),
                (new(-3047.7f, 587.6f, 7.9f), WeaponHash.Knife, 1),
                (new(-3043.2f, 583.9f, 7.9f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(-3243.9f, 1005.0f, 12.8f),
            [
                (new(-3245.0f, 1009.3f, 12.8f), WeaponHash.Knife, 1),
                (new(-3248.9f, 1005.8f, 12.8f), WeaponHash.Pistol, 5000),
                (new(-3246.5f, 1001.0f, 12.8f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(1138.1f, -981.5f, 46.4f),
            [
                (new(1130.5f, -982.7f, 46.4f), WeaponHash.Pistol, 5000),
                (new(1134.7f, -979.4f, 46.4f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(1159.0f, -323.1f, 69.2f),
            [
                (new(1153.3f, -321.3f, 69.2f), WeaponHash.Pistol, 5000),
                (new(1160.7f, -313.7f, 69.1f), WeaponHash.Knife, 1),
                (new(1163.9f, -319.2f, 69.2f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(377.6f, 326.9f, 103.5f),
            [
                (new(381.7f, 327.8f, 103.5f), WeaponHash.Pistol, 5000),
                (new(379.2f, 332.0f, 103.5f), WeaponHash.Pistol, 5000),
                (new(374.2f, 330.1f, 103.5f), WeaponHash.Knife, 1),
            ]
        ),
        (new(2555.3f, 385.8f, 108.6f),
            [
                (new(2553.9f, 389.5f, 108.6f), WeaponHash.Pistol, 5000),
                (new(2550.2f, 386.8f, 108.6f), WeaponHash.Pistol, 5000),
                (new(2553.2f, 381.9f, 108.6f), WeaponHash.Knife, 1),
            ]
        ),
        (new(1166.5f, 2707.2f, 38.1f),
            [
                (new(1166.0f, 2714.1f, 38.1f), WeaponHash.Pistol, 5000),
                (new(1168.7f, 2709.8f, 38.1f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(544.1f, 2669.2f, 42.1f),
            [
                (new(540.6f, 2666.6f, 42.1f), WeaponHash.Knife, 1),
                (new(545.0f, 2663.4f, 42.1f), WeaponHash.Pistol, 5000),
                (new(548.8f, 2667.0f, 42.1f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(1963.7f, 3743.5f, 32.3f),
            [
                (new(1965.9f, 3747.5f, 32.3f), WeaponHash.Pistol, 5000),
                (new(1961.1f, 3749.2f, 32.3f), WeaponHash.Pistol, 5000),
                (new(1958.9f, 3744.0f, 32.3f), WeaponHash.Knife, 1),
            ]
        ),
        (new(1392.5f, 3603.0f, 34.9f),
            [
                (new(1397.7f, 3607.0f, 34.9f), WeaponHash.Pistol, 5000),
                (new(1399.1f, 3603.6f, 34.9f), WeaponHash.Pistol, 5000),
                (new(1391.7f, 3608.4f, 34.9f), WeaponHash.Pistol, 5000),
                (new(1389.4f, 3604.5f, 34.9f), WeaponHash.Pistol, 5000),
            ]
        ),
        (new(2679.0f, 3284.6f, 55.2f),
            [
                (new(2679.4f, 3288.6f, 55.2f), WeaponHash.Pistol, 5000),
                (new(2673.7f, 3287.3f, 55.2f), WeaponHash.Pistol, 5000),
                (new(2674.5f, 3281.8f, 55.2f), WeaponHash.Knife, 1),
            ]
        ),
        (new(1702.0f, 4926.9f, 42.0f),
            [
                (new(1707.3f, 4919.7f, 42.0f), WeaponHash.Pistol, 5000),
                (new(1707.1f, 4930.2f, 42.0f), WeaponHash.Pistol, 5000),
                (new(1701.3f, 4922.5f, 42.0f), WeaponHash.Knife, 1),
            ]
        ),
        (new(1733.3f, 6414.4f, 35.0f),
            [
                (new(1736.9f, 6414.3f, 35.0f), WeaponHash.Pistol, 5000),
                (new(1736.0f, 6419.5f, 35.0f), WeaponHash.Pistol, 5000),
                (new(1730.2f, 6418.4f, 35.0f), WeaponHash.Knife, 1),
            ]
        )
    ];

    internal override void Setup()
    {
        index = Main.MersenneTwister.Next(robbersData.Length);
        CalloutPosition = robbersData[index].pos;
        CalloutMessage = Localization.GetString("StoreRobbery");
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.StoreRobbery, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, $"{Localization.GetString("StoreRobbery")} {Localization.GetString("RespondCode3")}");

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
            {
                foreach (var robber in robbers) if (robber is not null && robber.IsValid() && robber.Exists()) robber.Delete();
                if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
                if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
            }
            else
            {
                foreach (var robber in robbers) if (robber is not null && robber.IsValid() && robber.Exists()) robber.Dismiss();
                if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("StoreRobbery"));
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        RobbersModel.Load();
        HudHelpers.DisplayNotification($"{Localization.GetString("StoreRobberyDesc")} {Localization.GetString("RespondCode3")}", Localization.GetString("Dispatch"), Localization.GetString("StoreRobbery"));
        foreach (var (pos, weapon, ammo) in robbersData[index].robbersPos)
        {
            var robber = new Ped(RobbersModel, pos, 0f)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                Health = 250,
                MaxHealth = 250,
                Armor = 100,
                RelationshipGroup = RobbersRG,
            };
            if (robber is not null && robber.IsValid() && robber.Exists())
            {
                robber.Inventory.GiveNewWeapon(weapon, ammo, true);
            }
            robbers.Add(robber);
        }
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobbersRG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, RelationshipGroup.Cop, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RobbersRG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Respect);
        Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RelationshipGroup.Cop, Relationship.Respect);

        tbPool.Add(timerBar);
        blip = new(CalloutPosition, 30f)
        {
            Color = Color.Yellow,
            RouteColor = Color.Yellow,
            Alpha = 0.5f,
            IsRouteEnabled = true
        };

        GameFiber.StartNew(ProcessTimerBars);
        timer.Elapsed += (sender, e) =>
        {
            if (!arrived)
            {
                seconds--;
                var span = new TimeSpan(0, 0, seconds);
                timerBar.Text = span.ToString(@"hh\:mm\:ss");
            }
        };
        timer.Start();
    }

    internal override void NotAccepted() { }

    internal override void Update()
    {
        if (!arrived && (Vector3.Distance(Main.Player.Position, CalloutPosition) < 30f || seconds is <= 0))
        {
            if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
            pursuit = Functions.CreatePursuit();
            if (pursuit is not null)
            {
                foreach (var robber in robbers)
                {
                    if (robber is not null && robber.IsValid() && robber.Exists())
                    {
                        Functions.AddPedToPursuit(pursuit, robber);
                    }
                    robber.Tasks.FightAgainstClosestHatedTarget(100f, -1);
                }
                Functions.SetPursuitIsActiveForPlayer(pursuit, true);
            }
            Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS WE_HAVE CRIME_SUSPECT_ON_THE_RUN IN_OR_ON_POSITION", CalloutPosition);
            arrived = true;
        }

        if (Main.Player.IsDead) End();
        if (PedHelpers.IsAllPedDeadOrArrested([.. robbers])) End();
    }

    private void ProcessTimerBars()
    {
        while (!arrived && IsCalloutRunning)
        {
            GameFiber.Yield();
            tbPool.Draw();
        }
    }
}