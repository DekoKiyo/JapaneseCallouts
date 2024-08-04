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

    internal override void Setup()
    {
        CalloutPosition = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(Main.MT.Next(150, 650)));

        try
        {
            stolen = new(x => x.IsCar, CalloutPosition)
            {
                IsPersistent = true,
                IsStolen = true
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
            Main.Logger.Error(e.ToString());
        }

        CalloutMessage = Localization.GetString("StolenVehicle");

        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(Settings.StolenVehicleRadioSound, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("StolenVehicleDesc"));
        CalloutInterfaceAPIFunctions.SendVehicle(stolen);

        OnCalloutsEnded += () =>
        {
            if (Game.LocalPlayer.Character.IsDead)
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
                if (count > 10) Hud.DisplayNotification(Localization.GetString("Escaped"), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
                else Hud.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        Hud.DisplayNotification(Localization.GetString("StolenVehicleDesc"), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
        Hud.DisplayNotification(Localization.GetString("StolenVehicleData", stolen.LicensePlate, stolen.Class.ToString()), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
        if (stolen && stolen.IsValid() && stolen.Exists())
        {
            area = new(stolen.Position.Around(Main.MT.Next(100)), Main.MT.Next(75, 120))
            {
                Color = Color.Yellow,
                Alpha = 0.5f,
                IsRouteEnabled = true,
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

            Hud.DisplayNotification(Localization.GetString("GPSUpdate"));
            Hud.DisplayNotification(Localization.GetString("StolenVehicleData", stolen.LicensePlate, stolen.Class.ToString()));
            Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", stolen.Position);
            count++;
        }
        if (!found && Vector3.Distance(Game.LocalPlayer.Character.Position, stolen.Position) < 30f)
        {
            found = true;
            if (area is not null && area.IsValid() && area.Exists()) area.Delete();
            Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.StolenVehicle, stolen.Position);
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
        if (Game.LocalPlayer.Character.IsDead) End();
        if (suspect is not null && suspect.IsValid() && suspect.Exists() && (suspect.IsDead || Functions.IsPedArrested(suspect))) End();
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }
}