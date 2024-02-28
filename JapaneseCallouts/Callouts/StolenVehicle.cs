namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] StolenVehicle", CalloutProbability.Medium)]
internal class StolenVehicle : CalloutBase
{
    private Ped suspect;
    private Vehicle stolen;
    private LHandle pursuit;

    internal override void Setup()
    {
        CalloutPosition = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(Main.MersenneTwister.Next(250, 800)));

        try
        {
            stolen = new(x => x.IsCar, CalloutPosition);
            if (stolen is not null && stolen.IsValid() && stolen.Exists())
            {
                stolen.IsPersistent = true;
                stolen.IsStolen = true;
                suspect = stolen.CreateRandomDriver();
                if (suspect && suspect.IsValid() && suspect.Exists())
                {
                    suspect.IsPersistent = true;
                    suspect.BlockPermanentEvents = true;
                    suspect.Tasks.CruiseWithVehicle(40f, VehicleDrivingFlags.Emergency);

                    DeleteEntities().AddRange([stolen, suspect]);
                }
            }
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }

        CalloutMessage = Localization.GetString("StolenVehicle");

        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 30f);
        Functions.PlayScannerAudioUsingPosition("WE_HAVE JP_CRIME_STOLEN_VEHICLE IN_OR_ON_POSITION", CalloutPosition);

        if (Main.IsCalloutInterfaceAPIExist)
        {
            CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("StolenVehicleDesc"));
            CalloutInterfaceAPIFunctions.SendVehicle(stolen);
        }
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        HudExtensions.DisplayNotification(Localization.GetString("StolenVehicleDesc"));
        HudExtensions.DisplayNotification(Localization.GetString("StolenVehicleInfo", stolen.Model.Name, stolen.LicensePlate));
        try
        {
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
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }
    }

    internal override void Update()
    {
        GameFiber.Yield();
        if (Game.LocalPlayer.Character.IsDead) EndCallout(false, true);
        if (suspect is not null && suspect.IsValid() && suspect.Exists() && (suspect.IsDead || Functions.IsPedArrested(suspect))) EndCallout();
    }

    internal override void EndCallout(bool notAccepted = false, bool isPlayerDead = false)
    {
        if (isPlayerDead)
        {
            if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Delete();
            if (stolen is not null && stolen.IsValid() && stolen.Exists()) stolen.Delete();
        }
        else
        {
            if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Dismiss();
            if (stolen is not null && stolen.IsValid() && stolen.Exists()) stolen.Dismiss();
            HudExtensions.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("StolenVehicle"));
            Functions.PlayScannerAudio("ATTENTION_ALL_UNITS JP_WE_ARE_CODE4 JP_NO_FURTHER_UNITS_REQUIRED");
        }

        if (notAccepted)
        {
            if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Dismiss();
            if (stolen is not null && stolen.IsValid() && stolen.Exists()) stolen.Dismiss();
            if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
        }
    }
}