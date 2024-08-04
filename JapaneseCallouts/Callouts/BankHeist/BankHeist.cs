namespace JapaneseCallouts.Callouts.BankHeist;

[CalloutInfo("[JPC] Bank Heist", CalloutProbability.High)]
internal partial class BankHeist : CalloutBase<Configurations>
{
    private readonly RelationshipGroup RobberRG = new("ROBBER");
    private Blip BankBlip;
    private bool Arrived = false;
    private List<Ped> Robbers;
    private List<BlipPlus> EnemyBlips;

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("BankHeist");
        CalloutPosition = Vector3Helpers.GetNearestPos([.. BankData.Keys]);
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 100f);
        Functions.PlayScannerAudioUsingPosition(Settings.Instance.BankHeistRadioSound, CalloutPosition);

        Robbers = new(BankData[CalloutPosition].positions.Count());
        EnemyBlips = new(Robbers.Count());

        OnCalloutsEnded += () =>
        {
            foreach (var b in EnemyBlips) b?.Dismiss();
            foreach (var r in Robbers)
            {
                if (Game.LocalPlayer.Character.IsDead)
                {
                    if (r && r.IsValid() && r.Exists()) r.Delete();
                }
                else
                {
                    if (r && r.IsValid() && r.Exists()) r.Dismiss();
                }
            }
            if (!Game.LocalPlayer.Character.IsDead)
            {
                Hud.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("BankHeist"));
            }
        };
    }

    internal override void Accepted()
    {
        Hud.DisplayNotification(Localization.GetString("BankHeistDesc"));
        CalloutInterfaceAPIFunctions.SendMessage(this, $"{Localization.GetString("BankHeistDesc")} {Localization.GetString("RespondCode3")}");

        var weather = CalloutHelpers.GetWeatherType(IPTFunctions.GetWeatherType());

        foreach (var (pos, heading) in BankData[CalloutPosition].positions)
        {
            var pedData = CalloutHelpers.SelectPed(weather, [.. Configuration.Robbers]);
            var robber = new Ped(pedData.Model, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RobberRG,
                MaxHealth = pedData.Health,
                Health = pedData.Health,
                Armor = pedData.Armor,
            };
            if (robber is not null && robber.IsValid() && robber.Exists())
            {
                Natives.SET_PED_KEEP_TASK(robber, true);

                robber.SetOutfit(pedData);
                robber.GiveWeapon([.. Configuration.Weapons], true);

                Natives.SET_PED_SUFFERS_CRITICAL_HITS(robber, false);
                Robbers.Add(robber);
            }
        }

        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobberRG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobberRG, RelationshipGroup.Cop, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobberRG, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);

        BankBlip = new(CalloutPosition, 90f)
        {
            Alpha = 0.5f,
            Color = Color.Red,
            IsRouteEnabled = true,
        };
    }

    internal override void Update()
    {
        GameFiber.Yield();
        if (CalloutPosition.DistanceTo(Game.LocalPlayer.Character.Position) < 50f && !Arrived)
        {
            if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists())
            {
                BankBlip.IsRouteEnabled = false;
                BankBlip.Delete();
            }

            GameFiber.Wait(3000);
            foreach (var r in Robbers)
            {
                if (r is not null && r.IsValid() && r.Exists())
                {
                    r.Tasks.FightAgainstClosestHatedTarget(500f);
                    var eb = new BlipPlus(r, HudColor.Enemy.GetColor(), BlipSprite.Enemy);
                    EnemyBlips.Add(eb);
                }
            }
            Arrived = true;
        }

        foreach (var (pos, hash) in BankData[CalloutPosition].doors)
        {
            Natives.SET_LOCKED_UNSTREAMED_IN_DOOR_OF_TYPE(hash, pos.X, pos.Y, pos.Z, false, 0f, 0f, 0f);
        }

        if (Game.LocalPlayer.Character.IsDead) End();
        if (EntityHelpers.IsAllPedDeadOrArrested([.. Robbers])) End();
        if (KeyHelpers.IsKeysDown(Settings.Instance.EndCalloutsKey, Settings.Instance.EndCalloutsModifierKey)) End();
    }

    internal override void NotAccepted() { }
    internal override void OnDisplayed() { }
}