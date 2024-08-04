namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Escort", CalloutProbability.High)]
internal class Escort : CalloutBase
{
    private int index = 0;

    private Vehicle targetV, policeV1, policeV2;
    private Ped targetP, targetDriver, targetCop, police1P1, police1P2, police2P1, police2P2;
    private Blip startB, targetVB, policeV1B, policeV2B;

    private EscortStatus Status = EscortStatus.Init;

    private readonly MenuPool M_Pool = [];
    private UIMenu UIM_Config;
    private UIMenuListScrollerItem<string> UIL_TargetVehicle;
    private UIMenuListScrollerItem<string> UIL_PoliceVehicle1;
    private UIMenuListScrollerItem<string> UIL_PoliceVehicle2;
    private UIMenuListScrollerItem<string> UIL_TargetPed;
    private UIMenuListScrollerItem<string> UIL_PoliceOfficer;
    private UIMenuItem UII_Confirm;

    internal override void Setup()
    {
        var posL = new List<Vector3>();
        foreach (var pos in XmlManager.EscortConfig.Presets)
        {
            posL.Add(new(pos.TargetVehiclePosStart.X, pos.TargetVehiclePosStart.Y, pos.TargetVehiclePosStart.Z));
        }
        index = Vector3Helpers.GetNearestPosIndex(posL);
        CalloutPosition = posL[index];
        CalloutMessage = Localization.GetString("Escort");

        OnCalloutsEnded += () =>
        {

        };
    }

    internal override void Accepted()
    {
        Hud.DisplayNotification($"{Localization.GetString("EscortDesc")} {Localization.GetString("RespondCode3")}", Localization.GetString("Dispatch"), Localization.GetString("Escort"));

        // setup menu
        UIM_Config = new(Localization.GetString("EscortMenuTitle"), Main.PLUGIN_INFO)
        {
            MouseControlsEnabled = false,
            ControlDisablingEnabled = false,
        };
        UIL_TargetVehicle = new(Localization.GetString("EscortTargetVehicle"), "");
        UIL_PoliceVehicle1 = new(Localization.GetString("EscortPoliceVehicle1"), "");
        UIL_PoliceVehicle2 = new(Localization.GetString("EscortPoliceVehicle2"), "");
        UIL_TargetPed = new(Localization.GetString("EscortTargetPed"), "");
        UIL_PoliceOfficer = new(Localization.GetString("EscortPoliceOfficer"), "");
        UII_Confirm = new(Localization.GetString("Confirm"))
        {
            ForeColor = HudColor.Red.GetColor(),
            HighlightedBackColor = HudColor.Red.GetColor(),
        };
        UIM_Config.AddItems(UIL_TargetVehicle, UIL_PoliceVehicle1, UIL_PoliceVehicle2, UIL_TargetPed, UIL_PoliceOfficer, UII_Confirm);
        M_Pool.Add(UIM_Config);

        AddOptions([.. XmlManager.EscortConfig.TargetVehicles], UIL_TargetVehicle);
        AddOptions([.. XmlManager.EscortConfig.PoliceVehicles], UIL_PoliceVehicle1);
        AddOptions([.. XmlManager.EscortConfig.PoliceVehicles], UIL_PoliceVehicle2);
        AddOptions([.. XmlManager.EscortConfig.TargetPeds], UIL_TargetPed);
        AddOptions([.. XmlManager.EscortConfig.PoliceOfficers], UIL_PoliceOfficer);

        var weather = CalloutHelpers.GetWeatherType(IPTFunctions.GetWeatherType());

        GameFiber.StartNew(() =>
        {
            {
                var vData = XmlManager.EscortConfig.TargetVehicles[0];
                var posData = XmlManager.EscortConfig.Presets[index].TargetVehiclePosStart;
                targetV = new(vData.Model, new(posData.X, posData.Y, posData.Z), posData.Heading)
                {
                    IsPersistent = true,
                    DirtLevel = 0,
                };
                if (targetV is not null && targetV.IsValid() && targetV.Exists())
                {
                    targetV.ApplyTexture(vData);
                }
            }
            {
                var vData = XmlManager.EscortConfig.PoliceVehicles[0];
                var posData = XmlManager.EscortConfig.Presets[index].EscortPoliceVehicle1PosStart;
                policeV1 = new(vData.Model, new(posData.X, posData.Y, posData.Z), posData.Heading)
                {
                    IsPersistent = true,
                    DirtLevel = 0,
                };
                if (policeV1 is not null && policeV1.IsValid() && policeV1.Exists())
                {
                    policeV1.ApplyTexture(vData);
                }
            }
            {
                var vData = XmlManager.EscortConfig.PoliceVehicles[0];
                var posData = XmlManager.EscortConfig.Presets[index].EscortPoliceVehicle2PosStart;
                policeV2 = new(vData.Model, new(posData.X, posData.Y, posData.Z), posData.Heading)
                {
                    IsPersistent = true,
                    DirtLevel = 0,
                };
                if (policeV2 is not null && policeV2.IsValid() && policeV2.Exists())
                {
                    policeV2.ApplyTexture(vData);
                }
            }

            {
                var pData = XmlManager.EscortConfig.TargetPeds[0];
                targetP = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                    Opacity = 0,
                };
                if (targetP is not null && targetP.IsValid() && targetP.Exists())
                {
                    targetP.SetOutfit(pData);
                }
            }
            {
                var pData = XmlManager.EscortConfig.PoliceOfficers[0];
                targetDriver = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                };
                if (targetDriver is not null && targetDriver.IsValid() && targetDriver.Exists())
                {
                    targetDriver.SetOutfit(pData);
                    if (targetV is not null && targetV.IsValid() && targetV.Exists())
                    {
                        targetDriver.WarpIntoVehicle(targetV, -1);
                        targetDriver.Tasks.LeaveVehicle(LeaveVehicleFlags.None);
                    }
                }
                targetCop = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                };
                if (targetCop is not null && targetCop.IsValid() && targetCop.Exists())
                {
                    targetCop.SetOutfit(pData);
                    if (targetV is not null && targetV.IsValid() && targetV.Exists())
                    {
                        targetCop.WarpIntoVehicle(targetV, 0);
                    }
                }
                police1P1 = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                };
                if (police1P1 is not null && police1P1.IsValid() && police1P1.Exists())
                {
                    police1P1.SetOutfit(pData);
                    if (policeV1 is not null && policeV1.IsValid() && policeV1.Exists())
                    {
                        police1P1.WarpIntoVehicle(policeV1, -1);
                    }
                }
                police1P2 = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                };
                if (police1P2 is not null && police1P2.IsValid() && police1P2.Exists())
                {
                    police1P2.SetOutfit(pData);
                    if (policeV1 is not null && policeV1.IsValid() && policeV1.Exists())
                    {
                        police1P2.WarpIntoVehicle(policeV1, 0);
                    }
                }
                police2P1 = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                };
                if (police2P1 is not null && police2P1.IsValid() && police2P1.Exists())
                {
                    police2P1.SetOutfit(pData);
                    if (policeV2 is not null && policeV2.IsValid() && policeV2.Exists())
                    {
                        police2P1.WarpIntoVehicle(policeV2, -1);
                    }
                }
                police2P2 = new(pData.Model, new(), 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    Armor = 200,
                    Health = 200,
                };
                if (police2P2 is not null && police2P2.IsValid() && police2P2.Exists())
                {
                    police1P2.SetOutfit(pData);
                    if (policeV2 is not null && policeV2.IsValid() && policeV2.Exists())
                    {
                        police2P2.WarpIntoVehicle(policeV2, 0);
                    }
                }
            }
        });

        startB = new(CalloutPosition, 30f)
        {
            Color = HudColor.Yellow.GetColor(),
            RouteColor = HudColor.Yellow.GetColor(),
            IsRouteEnabled = true,
        };
    }

    internal override void NotAccepted() { }

    internal override void OnDisplayed() { }

    internal override void Update()
    {
        GameFiber.Yield();
        M_Pool.ProcessMenus();

        switch (Status)
        {
            case EscortStatus.Init:
                if (Vector3.Distance(CalloutPosition, Game.LocalPlayer.Character.Position) < 30f)
                {
                    if (startB is not null && startB.IsValid() && startB.Exists())
                    {
                        startB.Delete();
                    }
                    Status = EscortStatus.Arrived;
                }
                break;
            case EscortStatus.Arrived:
                break;
            case EscortStatus.Escorting:
                break;
            case EscortStatus.Completed:
                break;
        }
    }

    private static void AddOptions<T>(List<T> list, UIMenuListScrollerItem<string> item) where T : IModelObject
    {
        foreach (var option in list)
        {
            if (item.Items.Contains(option.Model))
            {
                for (int i = 1; ; i++)
                {
                    GameFiber.Yield();
                    var name = $"{option.Model} {i}";
                    if (item.Items.Contains(name)) continue;
                    item.Items.Add(name);
                    break;
                }
            }
            else
            {
                item.Items.Add(option.Model);
            }
        }
    }

    internal enum EscortStatus
    {
        Init = 0,
        Arrived,
        Escorting,
        Completed,
    }
}