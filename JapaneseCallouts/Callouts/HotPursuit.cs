namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Hot Pursuit", CalloutProbability.VeryHigh)]
internal class HotPursuit : CalloutBase
{
    private Vehicle vehicle;
    private Ped suspect;
    private LHandle pursuit;

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("HotPursuit");
        CalloutPosition = World.GetNextPositionOnStreet(Main.Player.Position.Around(350f, 750f));
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.HotPursuit, CalloutPosition);

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
            {
                if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Delete();
                if (vehicle is not null && vehicle.IsValid() && vehicle.Exists()) vehicle.Delete();
            }
            else
            {
                if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Dismiss();
                if (vehicle is not null && vehicle.IsValid() && vehicle.Exists()) vehicle.Dismiss();
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("HotPursuit"));
            }
        };
    }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("HotPursuitDesc"), Localization.GetString("Dispatch"), Localization.GetString("HotPursuit"));

        var vData = CalloutHelpers.Select([.. XmlManager.HotPursuitConfig.Vehicles]);
        vehicle = new(vData.Model, CalloutPosition);
        if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
        {
            vehicle.IsPersistent = true;
            vehicle.IsStolen = Main.MersenneTwister.Next(10) is 0;
            vehicle.ApplyTexture(vData);
            suspect = vehicle.CreateRandomDriver();
            pursuit = Functions.CreatePursuit();
            if (suspect is not null && suspect.IsValid() && suspect.Exists())
            {
                suspect.IsPersistent = true;
                suspect.BlockPermanentEvents = true;
                suspect.Tasks.CruiseWithVehicle(Main.MersenneTwister.Next(60, 81), VehicleDrivingFlags.Emergency);
                var pa = Functions.GetPedPursuitAttributes(suspect);
                if (pa is not null)
                {
                    pa.MinDrivingSpeed = 30f;
                    pa.MaxDrivingSpeed = 120f;
                    pa.HandlingAbility = 1.25f;
                    pa.HandlingAbilityTurns = 1.5f;
                    pa.SurrenderChanceCarBadlyDamaged = 10f;
                    pa.SurrenderChanceCarBadlyDamaged = 10f;
                    pa.SurrenderChancePitted = 5f;
                    pa.SurrenderChancePittedAndCrashed = 5f;
                    pa.SurrenderChancePittedAndSlowedDown = 5f;
                    pa.SurrenderChanceTireBurst = 10f;
                    pa.SurrenderChanceTireBurstAndCrashed = 10f;
                    pa.HandlingAbilityBurstTireMult = 0.5f;
                    pa.AverageBurstTireSurrenderTime = 250;
                    pa.BurstTireSurrenderMult = 0.2f;
                    pa.BurstTireMaxDrivingSpeedMult = 0.125f;
                    pa.AverageSurrenderTime = 50;
                    pa.AverageFightTime = 120;
                    pa.ExhaustionDuration = 50000;
                    pa.ExhaustionInterval = 50000;
                }
                if (pursuit is not null)
                {
                    Functions.AddPedToPursuit(pursuit, suspect);
                    Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                }

                Functions.RequestBackup(suspect.Position, EBackupResponseType.Pursuit, EBackupUnitType.LocalUnit);
                Functions.RequestBackup(suspect.Position, EBackupResponseType.Pursuit, EBackupUnitType.AirUnit);
            }
        }
    }

    internal override void Update()
    {
        GameFiber.Yield();
        if (Main.Player.IsDead) End();
        if (suspect is not null && suspect.IsValid() && suspect.Exists() && EntityHelpers.IsAllPedDeadOrArrested([suspect])) End();
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }

    internal override void NotAccepted() { }
    internal override void OnDisplayed() { }
}