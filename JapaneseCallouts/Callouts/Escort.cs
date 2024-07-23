namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Escort", CalloutProbability.High)]
internal class Escort : CalloutBase
{
    private int index = 0;

    private Vehicle targetV, policeV1, policeV2;
    private Ped targetP, targetDriver, targetCop, police1P1, police1P2, police2P1, police2P2;

    private MenuPool M_Pool = [];
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

        {
            var vData = CalloutHelpers.Select([.. XmlManager.EscortConfig.TargetVehicles]);
            var pData = XmlManager.EscortConfig.Presets[index].TargetVehiclePosStart;
            targetV = new(vData.Model, new(pData.X, pData.Y, pData.Z), pData.Heading)
            {
                IsPersistent = true,
                DirtLevel = 0,
            };
            if (targetV is not null && targetV.IsValid() && targetV.Exists())
            {
            }
        }
    }

    internal override void NotAccepted()
    {
    }

    internal override void OnDisplayed() { }

    internal override void Update()
    {
        GameFiber.Yield();
        M_Pool.ProcessMenus();
    }

    private static void AddOptions<T>(List<T> list, UIMenuListScrollerItem<string> item) where T : IEntityObject
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
}