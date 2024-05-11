namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Bank Heist", CalloutProbability.High)]
internal class BankHeist : CalloutBase
{
    private readonly RelationshipGroup RobberRG = new("ROBBER");
    private Blip BankBlip;
    private bool Arrived = false;
    private readonly List<Ped> Robbers = [];
    private int Index = 0;

    internal override void Setup()
    {
        var posList = new List<Vector3>();
        foreach (var pos in XmlManager.BankHeistConfig.BankData)
        {
            posList.Add(new(pos.X, pos.Y, pos.Z));
        }
        Index = posList.GetNearestPosIndex();
        CalloutMessage = Localization.GetString("BankHeist");
        CalloutPosition = posList[Index];
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 100f);
        Functions.PlayScannerAudioUsingPosition("", CalloutPosition);

        OnCalloutsEnded += () =>
        {
            foreach (var r in Robbers)
            {
                if (Main.Player.IsDead)
                {
                    if (r && r.IsValid() && r.Exists()) r.Delete();
                }
                else
                {
                    if (r && r.IsValid() && r.Exists()) r.Dismiss();
                }
            }
            if (!Main.Player.IsDead)
            {
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("BankHeist"));
                Functions.PlayScannerAudio("ATTENTION_ALL_UNITS DL_CODE4 DL_NO_FURTHER_UNITS_REQUIRED");
            }
        };
    }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("BankHeistDesc"));
        CalloutInterfaceAPIFunctions.SendMessage(this, $"{Localization.GetString("BankHeist")} {Localization.GetString("RespondCode3")}");

        foreach (var pos in XmlManager.BankHeistConfig.BankData[Index].RobbersPos)
        {
            var pedData = CalloutHelpers.Select([.. XmlManager.BankHeistConfig.Robbers]);
            var robber = new Ped(pedData.Model, new(pos.X, pos.Y, pos.Z), pos.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RobberRG,
                KeepTasks = true,
                MaxHealth = pedData.Health,
                Health = pedData.Health,
                Armor = pedData.Armor,
            };
            if (robber is not null && robber.IsValid() && robber.Exists())
            {
                robber.SetOutfit(pedData);
                var weapon = CalloutHelpers.Select([.. XmlManager.BankHeistConfig.Weapons]);
                var hash = NativeFunction.Natives.GET_HASH_KEY<Model>(weapon.Model);
                NativeFunction.Natives.REQUEST_MODEL(hash);
                NativeFunction.Natives.GIVE_WEAPON_TO_PED(robber, hash, 5000, false, true);
                foreach (var comp in weapon.Components)
                {
                    var compHash = NativeFunction.Natives.GET_HASH_KEY<Model>(comp);
                    NativeFunction.Natives.REQUEST_MODEL(compHash);
                    NativeFunction.Natives.GIVE_WEAPON_COMPONENT_TO_PED(robber, hash, compHash);
                }

                NativeFunction.Natives.SET_PED_SUFFERS_CRITICAL_HITS(robber, false);
                Robbers.Add(robber);
            }
        }

        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobberRG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobberRG, RelationshipGroup.Cop, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RobberRG, Main.Player.RelationshipGroup, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Respect);

        BankBlip = new(CalloutPosition, 90f);
        if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists())
        {
            BankBlip.Alpha = 0.5f;
            BankBlip.Color = Color.Red;
            BankBlip.EnableRoute(Color.Red);
        }
    }

    internal override void Update()
    {
        GameFiber.Yield();
        if (CalloutPosition.DistanceTo(Main.Player.Position) < 50f && !Arrived)
        {
            if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists())
            {
                BankBlip.DisableRoute();
                BankBlip.Delete();
            }

            GameFiber.Wait(3000);
            foreach (var r in Robbers)
            {
                r.Tasks.FightAgainstClosestHatedTarget(500f);
            }
            Arrived = true;
        }

        if (Main.Player.IsDead) End();
        if (EntityHelpers.IsAllPedDeadOrArrested([.. Robbers])) End();
    }

    internal override void NotAccepted() { }
    internal override void OnDisplayed() { }
}