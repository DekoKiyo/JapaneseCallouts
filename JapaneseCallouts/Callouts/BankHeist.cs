namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Bank Heist", CalloutProbability.High)]
internal class BankHeist : CalloutBase
{
    private readonly RelationshipGroup RobberRG = new("ROBBER");
    private Blip BankBlip;
    private bool Arrived = false;
    private readonly List<Ped> Robbers = [];

    private static readonly Dictionary<Vector3, (List<(Vector3 pos, float heading)> positions, List<(Vector3 pos, uint hash)> doors)> BankData = new()
    {
        {
            new(149.0f, -1042.7f, 29.3f),
            (
                [
                    (new(152.56f, -1041.58f, 30f), 8.51f),
                    (new(144.43f, -1038.80f, 30f), 270.83f),
                    (new(143.04f, -1043.66f, 30f), 335.35f),
                    (new(146.29f, -1045.20f, 30f), 60.17f),
                ],
                [
                    (new(149.6298f, -1037.231f, 29.71915f), 3142793112),
                    (new(152.0632f, -1038.124f, 29.71909f), 73386408),
                ]
            )
        },
        {
            new(317.0f, -279.8f, 54.2f),
            (
                [
                    (new(317.0f, -279.8f, 54.2f), 27.9f),
                    (new(308.9f, -277.1f, 54.2f), 274.0f),
                    (new(307.4f, -281.8f, 54.2f), 303.7f),
                    (new(311.59f, -283.51f, 54.16f), 69.2f),
                ],
                [
                    (new(313.9587f, -275.5965f, 54.51586f), 3142793112),
                    (new(316.3925f, -276.4888f, 54.5158f), 73386408),
                ]
            )
        }
    };

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("BankHeist");
        CalloutPosition = Vector3Helpers.GetNearestPos([.. BankData.Keys]);
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 100f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.BankHeist, CalloutPosition);

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
            }
        };
    }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("BankHeistDesc"));
        CalloutInterfaceAPIFunctions.SendMessage(this, $"{Localization.GetString("BankHeist")} {Localization.GetString("RespondCode3")}");

        foreach (var (pos, heading) in BankData[CalloutPosition].positions)
        {
            var pedData = CalloutHelpers.Select([.. XmlManager.BankHeistConfig.Robbers]);
            var robber = new Ped(pedData.Model, pos, heading)
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
                robber.GiveWeapon([.. XmlManager.BankHeistConfig.Weapons]);

                NativeFunction.Natives.SET_PED_SUFFERS_CRITICAL_HITS(robber, false);
                Robbers.Add(robber);
            }
        }

        foreach (var (pos, hash) in BankData[CalloutPosition].doors)
        {
            NativeFunction.CallByHash<uint>(Main._DOOR_CONTROL, hash, pos.X, pos.Y, pos.Z, false, 0f, 0f, 0f);
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