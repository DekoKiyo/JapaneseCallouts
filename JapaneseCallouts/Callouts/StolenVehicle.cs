namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Stolen Vehicle", CalloutProbability.VeryHigh)]
internal class StolenVehicle : CalloutBase
{
    private Ped suspect;
    private Vehicle stolen;
    private Blip area;
    private LHandle pursuit;
    private int blipTimer = 750;
    private int count = 0;
    private bool found = false;
    private (string name, Color color) ColorData;
    private readonly (string name, Color color)[] ColorsData =
    [
        (Localization.GetString("Black"), Color.FromArgb(0, 0, 0)),
        (Localization.GetString("Blue"), Color.FromArgb(0, 0, 255)),
        (Localization.GetString("Brown"), Color.FromArgb(165, 42, 42)),
        (Localization.GetString("Green"), Color.FromArgb(0, 120, 0)),
        (Localization.GetString("Orange"), Color.FromArgb(255, 165, 0)),
        (Localization.GetString("Purple"), Color.FromArgb(255, 0, 255)),
        (Localization.GetString("Red"), Color.FromArgb(255, 0, 0)),
        (Localization.GetString("White"), Color.FromArgb(255, 255, 255)),
        (Localization.GetString("Yellow"), Color.FromArgb(255, 255, 0)),
        (Localization.GetString("LiteBlue"), Color.FromArgb(0, 255, 255)),
        (Localization.GetString("Gray"), Color.FromArgb(211, 211, 211)),
    ];

    internal override void Setup()
    {
        CalloutPosition = World.GetNextPositionOnStreet(Main.Player.Position.Around(Main.MersenneTwister.Next(150, 650)));

        try
        {
            ColorData = ColorsData[Main.MersenneTwister.Next(ColorsData.Length)];
            stolen = new(x => x.IsCar, CalloutPosition)
            {
                IsPersistent = true,
                IsStolen = true,
                PrimaryColor = ColorData.color,
                SecondaryColor = ColorData.color
            };
            if (stolen is not null && stolen.IsValid() && stolen.Exists())
            {
                suspect = stolen.CreateRandomDriver();
                if (suspect && suspect.IsValid() && suspect.Exists())
                {
                    suspect.IsPersistent = true;
                    suspect.BlockPermanentEvents = true;
                    suspect.Tasks.CruiseWithVehicle(40f, VehicleDrivingFlags.Emergency);
                }
            }
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }

        CalloutMessage = Localization.GetString("StolenVehicle");

        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.StolenVehicle, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("StolenVehicle"));
        CalloutInterfaceAPIFunctions.SendVehicle(stolen);

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
            {
                if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Delete();
                if (stolen is not null && stolen.IsValid() && stolen.Exists()) stolen.Delete();
                if (area is not null && area.IsValid() && area.Exists()) area.Delete();
                if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
            }
            else
            {
                if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Dismiss();
                if (stolen is not null && stolen.IsValid() && stolen.Exists()) stolen.Dismiss();
                if (area is not null && area.IsValid() && area.Exists()) area.Delete();
                if (count > 10) HudHelpers.DisplayNotification(Localization.GetString("Escaped"), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
                else HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("StolenVehicleDesc"));
        HudHelpers.DisplayNotification(Localization.GetString("StolenVehicleData"), stolen.LicensePlate, ColorData.name, stolen.Class.ToString());
        if (stolen && stolen.IsValid() && stolen.Exists())
        {
            area = new(stolen.Position.Around(Main.MersenneTwister.Next(100)), Main.MersenneTwister.Next(75, 120))
            {
                Color = Color.Yellow,
                Alpha = 0.5f,
                IsRouteEnabled = true
            };
        }
    }

    internal override void NotAccepted()
    {
        if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Delete();
        if (stolen is not null && stolen.IsValid() && stolen.Exists()) stolen.Delete();
        if (area is not null && area.IsValid() && area.Exists()) area.Delete();
        if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
    }

    internal override void Update()
    {
        if (!found && !IPTFunctions.IsGamePaused()) blipTimer--;
        if (blipTimer < 0 && !found)
        {
            blipTimer = 1800;
            area.IsRouteEnabled = false;
            area.Position = stolen.Position;
            area.IsRouteEnabled = true;

            HudHelpers.DisplayNotification(Localization.GetString("StolenVehicleDataUpdate"));
            HudHelpers.DisplayNotification(Localization.GetString("StolenVehicleData", stolen.LicensePlate, ColorData.name, stolen.Class.ToString()));
            Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", stolen.Position);
            count++;
        }
        if (!found && Vector3.Distance(Main.Player.Position, stolen.Position) < 20f)
        {
            found = true;
            if (area is not null && area.IsValid() && area.Exists()) area.Delete();
            Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS WE_HAVE JP_CRIME_STOLEN_VEHICLE IN_OR_ON_POSITION", stolen.Position);
            if (suspect is not null && suspect.IsValid() && suspect.Exists())
            {
                pursuit = Functions.CreatePursuit();
                if (pursuit is not null)
                {
                    Functions.AddPedToPursuit(pursuit, suspect);
                    Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                }
            }
        }
        if (count > 15) End();
        if (Main.Player.IsDead) End();
        if (suspect is not null && suspect.IsValid() && suspect.Exists() && (suspect.IsDead || Functions.IsPedArrested(suspect))) End();
        if (found && !Functions.IsPursuitStillRunning(pursuit)) End();
    }
}