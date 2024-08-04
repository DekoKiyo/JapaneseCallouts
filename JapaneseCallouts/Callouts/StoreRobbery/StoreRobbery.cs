namespace JapaneseCallouts.Callouts.StoreRobbery;

[CalloutInfo("[JPC] Store Robbery", CalloutProbability.VeryHigh)]
internal class StoreRobbery : CalloutBase<Configurations>
{
    private int index = 0, seconds = 80;
    private List<Ped> robbers;
    private readonly Model RobbersModel = "MP_G_M_PROS_01";
    private readonly RelationshipGroup RobbersRG = "ROBBERS";
    private bool arrived = false;
    private LHandle pursuit;
    private Blip blip;
    private readonly Timer timer = new(1000);
    private readonly TimerBarPool tbPool = [];
    private readonly TextTimerBar timerBar = new(Localization.GetString("Remaining"), "");

    internal override void Setup()
    {
        var list = new List<Vector3>();
        foreach (var store in Configuration.Stores)
        {
            list.Add(new(store.X, store.Y, store.Z));
        }
        index = list.GetNearestPosIndex();
        robbers = new(Configuration.Stores[index].RobbersPositions.Count());
        CalloutPosition = list[index];
        CalloutMessage = Localization.GetString("StoreRobbery");
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(Settings.Instance.StoreRobberyRadioSound, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, $"{Localization.GetString("StoreRobberyDesc")} {Localization.GetString("RespondCode3")}");

        OnCalloutsEnded += () =>
        {
            if (Game.LocalPlayer.Character.IsDead)
            {
                foreach (var robber in robbers) if (robber is not null && robber.IsValid() && robber.Exists()) robber.Delete();
                if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
                if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
            }
            else
            {
                foreach (var robber in robbers) if (robber is not null && robber.IsValid() && robber.Exists()) robber.Dismiss();
                if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
                Hud.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("StoreRobbery"));
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        RobbersModel.Load();
        Hud.DisplayNotification($"{Localization.GetString("StoreRobberyDesc")} {Localization.GetString("RespondCode3")}", Localization.GetString("Dispatch"), Localization.GetString("StoreRobbery"));
        foreach (var rp in XmlManager.StoreRobberyConfig.Stores[index].RobbersPositions)
        {
            var robber = new Ped(RobbersModel, new(rp.X, rp.Y, rp.Z), 0f)
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
                robber.GiveWeapon([.. XmlManager.StoreRobberyConfig.Weapons], true);
                robbers.Add(robber);
            }
        }
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobbersRG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, RelationshipGroup.Cop, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(Game.LocalPlayer.Character.RelationshipGroup, RobbersRG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        Game.SetRelationshipBetweenRelationshipGroups(Game.LocalPlayer.Character.RelationshipGroup, RelationshipGroup.Cop, Relationship.Respect);

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
        if (!arrived && (Vector3.Distance(Game.LocalPlayer.Character.Position, CalloutPosition) < 30f || seconds is <= 0))
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

        if (Game.LocalPlayer.Character.IsDead) End();
        if (EntityHelpers.IsAllPedDeadOrArrested([.. robbers])) End();
        if (KeyHelpers.IsKeysDown(Settings.Instance.EndCalloutsKey, Settings.Instance.EndCalloutsModifierKey)) End();
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