namespace JapaneseCallouts.Callouts.BankHeist;

[CalloutInfo("[JPC] Bank Heist", CalloutProbability.High)]
internal class BankHeist : CalloutBase
{
    private readonly RelationshipGroup RobberRG = new("ROBBER");
    private Blip BankBlip;
    private bool Arrived = false;
    private List<Ped> Robbers;
    private List<BlipPlus> EnemyBlips;

    private static readonly Dictionary<Vector3, ((Vector3 pos, float heading)[] positions, (Vector3 pos, uint hash)[] doors)> BankData = new()
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
                    (new(150.2913f, -1047.629f, 29.6663f), 2703963187),
                    (new(148.0266f, -1044.364f, 29.50693f), 2121050683),
                    (new(145.4186f, -1041.813f, 29.64255f), 4163212883),
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
                    (new(314.6238f, -285.9945f, 54.46301f), 2703963187),
                    (new(312.358f, -282.7301f, 54.30365f), 2121050683),
                    (new(309.7491f, -280.1797f, 54.43926f), 4163212883),
                ]
            )
        },
        {
            new(-348.2f, -50.6f, 49.0f),
            (
                [
                    (new(-348.2f, -50.6f, 49.0f), 36.3f),
                    (new(-356.2f, -48.0f, 49.0f), 276.0f),
                    (new(-357.7f, -52.8f, 49.0f), 340.6f),
                    (new(-353.7f, -54.4f, 49.0f), 70.8f),
                ],
                [
                    (new(-351.2598f, -46.41221f, 49.38765f), 3142793112),
                    (new(-348.8109f, -47.26213f, 49.38759f), 73386408),
                    (new(-350.4144f, -56.79705f, 49.3348f), 2703963187),
                    (new(-352.7365f, -53.57248f, 49.17543f), 2121050683),
                    (new(-355.3892f, -51.06768f, 49.31105f), 4163212883),
                ]
            )
        },
        {
            new(-2962.7f, 486.0f, 15.7f),
            (
                [
                    (new(-2962.7f, 486.0f, 15.7f), 137.0f),
                    (new(-2962.9f, 477.8f, 15.7f), 31.5f),
                    (new(-2958.0f, 477.4f, 15.7f), 86.5f),
                    (new(-2957.4f, 481.3f, 15.7f), 169.1f),
                ],
                [
                    (new(-2965.71f, 484.2195f, 16.0481f), 73386408),
                    (new(-2965.821f, 481.6297f, 16.04816f), 3142793112),
                    (new(-2956.116f, 485.4206f, 15.99531f), 2703963187),
                    (new(-2958.538f, 482.2705f, 15.83594f), 2121050683),
                    (new(-2960.176f, 479.0105f, 15.97156f), 4163212883),
                ]
            )
        },
        {
            new(-1210.3f, -329.2f, 37.8f),
            (
                [
                    (new(-1210.3f, -329.2f, 37.8f), 80.0f),
                    (new(-1217.9f, -332.4f, 37.8f), 306.3f),
                    (new(-1215.1f, -337.4f, 37.8f), 33.7f),
                    (new(-1211.5f, -335.8f, 37.8f), 116.1f),
                ],
                [
                    (new(-1215.386f, -328.5237f, 38.13211f), 3142793112),
                    (new(-1213.074f, -327.3524f, 38.13205f), 73386408),
                    (new(-1207.328f, -335.1289f, 38.07925f), 2703963187),
                    (new(-1211.261f, -334.5596f, 37.91989f), 2121050683),
                    (new(-1214.906f, -334.7281f, 38.05551f), 4163212883),
                ]
            )
        },
        {
            new(1172.4f, 2706.5f, 38.1f),
            (
                [
                    (new(1172.4f, 2706.5f, 38.1f), 226.9f),
                    (new(1180.7f, 2706.4f, 38.1f), 108.6f),
                    (new(1180.4f, 2711.4f, 38.1f), 181.2f),
                    (new(1176.7f, 2711.7f, 38.1f), 269.6f),
                ],
                [
                    (new(1173.903f, 2703.613f, 38.43904f), 73386408),
                    (new(1176.495f, 2703.613f, 38.43911f), 3142793112),
                    (new(1172.291f, 2713.146f, 38.38625f), 2703963187),
                    (new(1175.542f, 2710.861f, 38.22689f), 2121050683),
                    (new(1178.87f, 2709.365f, 38.36251f), 4163212883),
                ]
            )
        },
    };

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("BankHeist");
        CalloutPosition = Vector3Helpers.GetNearestPos([.. BankData.Keys]);
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 100f);
        Functions.PlayScannerAudioUsingPosition(Settings.BankHeistRadioSound, CalloutPosition);

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
            var pedData = CalloutHelpers.SelectPed(weather, [.. XmlManager.BankHeistConfig.Robbers]);
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
                robber.GiveWeapon([.. XmlManager.BankHeistConfig.Weapons], true);

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
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }

    internal override void NotAccepted() { }
    internal override void OnDisplayed() { }
}