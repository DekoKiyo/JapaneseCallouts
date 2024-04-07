namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Road Rage", CalloutProbability.Medium)]
internal class RoadRage : CalloutBase
{
    private Vehicle suspectV, victimV;
    private Ped suspect, victim;
    private Blip area, victimB;
    private LHandle pursuit;
    private bool found = false, arrested = false, arrived = false, talkedToVictim = false;
    private int blipTimer = 750, count = 0;

    private static readonly (string, string)[] FinalVictimTalk =
    [
        (Localization.GetString("Victim"), Localization.GetString("RoadRageFinal1")),
        (Settings.OfficerName, Localization.GetString("RoadRageFinal2")),
        (Localization.GetString("Victim"), Localization.GetString("RoadRageFinal3")),
        (Settings.OfficerName, Localization.GetString("RoadRageFinal4")),
        (Localization.GetString("Victim"), Localization.GetString("RoadRageFinal5")),
    ];

    internal override void Setup()
    {
        CalloutPosition = World.GetNextPositionOnStreet(Main.Player.Position.Around(Main.MersenneTwister.Next(450, 800)));

        suspectV = new(x => x.IsCar && !x.IsEmergencyVehicle, CalloutPosition)
        {
            IsPersistent = true,
        };
        victimV = new(x => x.IsCar && !x.IsEmergencyVehicle, suspectV.GetOffsetPositionFront(5f))
        {
            IsPersistent = true,
        };
        victim = new(x => x.IsPed)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
        };
        suspect = new(x => x.IsPed)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
        };
        if (victim is not null && victim.IsValid() && victim.Exists() &&
            victimV is not null && victimV.IsValid() && victimV.Exists())
        {
            victim.WarpIntoVehicle(victimV, -1);
            victim.Tasks.CruiseWithVehicle(victimV, 10f, VehicleDrivingFlags.Emergency);

            if (suspect is not null && suspect.IsValid() && suspect.Exists() &&
                suspectV is not null && suspectV.IsValid() && suspectV.Exists())
            {
                suspect.WarpIntoVehicle(suspectV, -1);
                suspect.Tasks.ChaseWithGroundVehicle(victim);
            }
        }

        CalloutMessage = Localization.GetString("RoadRage");
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT JP_CRIME_TRAFFIC_VIOLATION IN_OR_ON_POSITION", CalloutPosition);

        if (Main.IsCalloutInterfaceAPIExist)
        {
            CalloutInterfaceAPIFunctions.SendMessage(this, $"{Localization.GetString("RoadRage")} {Localization.GetString("RespondCode2")}");
            CalloutInterfaceAPIFunctions.SendVehicle(suspectV);
            CalloutInterfaceAPIFunctions.SendVehicle(victimV);
        }

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
            {
                if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Delete();
                if (victim is not null && victim.IsValid() && victim.Exists()) victim.Delete();
                if (suspectV is not null && suspectV.IsValid() && suspectV.Exists()) suspectV.Delete();
                if (victimV is not null && victimV.IsValid() && victimV.Exists()) victimV.Delete();
                if (victimB is not null && victimB.IsValid() && victimB.Exists()) victimB.Delete();
                if (area is not null && area.IsValid() && area.Exists()) area.Delete();
                if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
            }
            else
            {
                if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Dismiss();
                if (victim is not null && victim.IsValid() && victim.Exists()) victim.Dismiss();
                if (suspectV is not null && suspectV.IsValid() && suspectV.Exists()) suspectV.Dismiss();
                if (victimV is not null && victimV.IsValid() && victimV.Exists()) victimV.Dismiss();
                if (victimB is not null && victimB.IsValid() && victimB.Exists()) victimB.Delete();
                if (area is not null && area.IsValid() && area.Exists()) area.Delete();
                if (count > 10) HudHelpers.DisplayNotification(Localization.GetString("Escaped"), Localization.GetString("Dispatch"), Localization.GetString("RoadRage"));
                else HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("RoadRage"));
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification($"{Localization.GetString("RoadRage")} {Localization.GetString("RespondCode2")}", Localization.GetString("Dispatch"), Localization.GetString("RoadRage"));
        area = new(victimV.Position.Around(Main.MersenneTwister.Next(100)), Main.MersenneTwister.Next(75, 120))
        {
            Color = Color.Yellow,
            Alpha = 0.5f,
            IsRouteEnabled = true
        };
    }

    internal override void NotAccepted()
    {
        if (suspect is not null && suspect.IsValid() && suspect.Exists()) suspect.Delete();
        if (victim is not null && victim.IsValid() && victim.Exists()) victim.Delete();
        if (suspectV is not null && suspectV.IsValid() && suspectV.Exists()) suspectV.Delete();
        if (victimV is not null && victimV.IsValid() && victimV.Exists()) victimV.Delete();
        if (area is not null && area.IsValid() && area.Exists()) area.Delete();
        if (victimB is not null && victimB.IsValid() && victimB.Exists()) victimB.Delete();
        if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
    }

    internal override void Update()
    {
        if (!found && !IPTFunctions.IsGamePaused()) blipTimer--;
        if (blipTimer < 0 && !found)
        {
            blipTimer = 1200;
            area.IsRouteEnabled = false;
            area.Position = victimV.Position;
            area.IsRouteEnabled = true;

            HudHelpers.DisplayNotification(Localization.GetString("StolenVehicleDataUpdate"));
            NativeFunction.Natives.GET_STREET_NAME_AT_COORD(victimV.Position.X, victimV.Position.Y, victimV.Position.Z, out uint hash, out uint _);
            var streetName = NativeFunction.Natives.GET_STREET_NAME_FROM_HASH_KEY<string>(hash);
            HudHelpers.DisplayNotification(Localization.GetString("TargetIsIn", streetName));
            Functions.PlayScannerAudioUsingPosition("JP_TARGET_IS IN_OR_ON_POSITION", victimV.Position);
            count++;
        }
        if (!found && Vector3.Distance(Main.Player.Position, victimV.Position) < 20f)
        {
            found = true;
            if (area is not null && area.IsValid() && area.Exists()) area.Delete();
            Functions.PlayScannerAudioUsingPosition("ATTENTION_ALL_UNITS WE_HAVE CRIME_SUSPECT_ON_THE_RUN IN_OR_ON_POSITION", victimV.Position);
            if (suspect is not null && suspect.IsValid() && suspect.Exists() &&
                victim is not null && victim.IsValid() && victim.Exists())
            {
                victim.Tasks.Clear();
                suspect.Tasks.Clear();
                victim.Tasks.ParkVehicle(victim.Position, victim.Heading).WaitForCompletion(10000);
                victimB = victim.AttachBlip();
                if (victimB is not null && victimB.IsValid() && victimB.Exists())
                {
                    victimB.Color = Color.Green;
                    victimB.RouteColor = Color.Green;
                }
                pursuit = Functions.CreatePursuit();
                if (pursuit is not null)
                {
                    Functions.AddPedToPursuit(pursuit, suspect);
                    Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                }
            }
        }

        if (!arrested && suspect is not null && suspect.IsValid() && suspect.Exists() && (suspect.IsDead || Functions.IsPedArrested(suspect)))
        {
            arrested = true;
            HudHelpers.DisplayHelp(Localization.GetString("TalkToGetInfo", Localization.GetString("Victim")));
            if (victimB is not null && victimB.IsValid() && victimB.Exists())
            {
                victimB.IsRouteEnabled = true;
            }
        }
        if (arrested)
        {
            if (!arrived && victim is not null && victim.IsValid() && victim.Exists())
            {
                if (Vector3.Distance(victim.Position, Main.Player.Position) < 20f)
                {
                    if (victimB is not null && victimB.IsValid() && victimB.Exists())
                    {
                        victimB.IsRouteEnabled = false;
                        arrived = true;
                    }
                }
            }

            if (arrived)
            {
                if (!talkedToVictim)
                {
                    if (!Main.Player.IsInAnyVehicle(false))
                    {
                        if (Vector3.Distance(Main.Player.Position, victim.Position) < 4f)
                        {
                            KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Victim")], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                            {
                                Conversations.Talk(FinalVictimTalk);
                                End();
                            }
                        }
                    }
                }
                if (!victim.Exists() && !victimV.Exists()) End();
            }
        }
        if (count > 10) End();
        if (Main.Player.IsDead) End();
    }
}