namespace JapaneseCallouts.Callouts.DrunkGuys;

[CalloutInfo("[JPC] Drunk Guys", CalloutProbability.High)]
internal class DrunkGuys : CalloutBase<Configurations>
{
    private Ped citizen;
    private Blip citizenB;
    private List<Ped> peds;
    private bool arrived = false;
    private const string CLIP_SET = "MOVE_M@DRUNK@VERYDRUNK";
    private const string ANIM_DICTIONARY = "missarmenian2";
    private const string ANIM_TYPE1 = "corpse_search_exit_ped";
    private const string ANIM_TYPE2 = "drunk_loop";
    private DrunkGuysPosition calloutData;

    private readonly (string, string)[] TalkToCitizen =
    [
        (Settings.Instance.OfficerName, Localization.GetString("DrunkCitizen1")),
        (Localization.GetString("Citizen"), Localization.GetString("DrunkCitizen2")),
        (Localization.GetString("Citizen"), Localization.GetString("DrunkCitizen3")),
        (Settings.Instance.OfficerName, Localization.GetString("DrunkCitizen4")),
        (Localization.GetString("Citizen"), Localization.GetString("DrunkCitizen5")),
    ];

    internal override void Setup()
    {
        var positions = new List<Vector3>();
        foreach (var p in Configuration.DrunkGuysPositions)
        {
            positions.Add(new(p.X, p.Y, p.Z));
        }
        var index = Vector3Helpers.GetNearestPosIndex(positions);
        calloutData = Configuration.DrunkGuysPositions[index];
        peds = new(calloutData.DrunkPos.Length);
        CalloutPosition = positions[index];
        CalloutMessage = Localization.GetString("DrunkGuys");
        NoLastRadio = true;
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 20f);
        Functions.PlayScannerAudioUsingPosition(Settings.Instance.DrunkGuysRadioSound, CalloutPosition);

        OnCalloutsEnded += () =>
        {
            if (Game.LocalPlayer.Character.IsDead)
            {
                if (citizen is not null && citizen.IsValid() && citizen.Exists()) citizen.Delete();
                foreach (var ped in peds) if (ped is not null && ped.IsValid() && ped.Exists()) ped.Delete();
                if (citizenB is not null && citizenB.IsValid() && citizenB.Exists()) citizenB.Delete();
                Hud.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("DrunkGuys"));
            }
            else
            {
                if (citizenB is not null && citizenB.IsValid() && citizenB.Exists()) citizenB.Delete();
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        Hud.DisplayNotification(Localization.GetString("DrunkGuysDesc"), Localization.GetString("Dispatch"), Localization.GetString("DrunkGuys"));
        citizen = new(CalloutPosition, calloutData.Heading)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
        };
        if (citizen is not null && citizen.IsValid() && citizen.Exists())
        {
            if (Main.IsSTPRunning)
            {
                StopThePedFunctions.SetPedAlcoholOverLimit(citizen, true);
            }
            Natives.SET_PED_KEEP_TASK(citizen, true);

            citizenB = citizen.AttachBlip();
            citizenB.Color = Color.Green;
            citizenB.IsRouteEnabled = true;
        }
        foreach (var data in calloutData.DrunkPos)
        {
            var cus = new Ped(x => x.IsPed, new(data.X, data.Y, data.Z), data.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                MovementAnimationSet = CLIP_SET
            };
            if (cus is not null && cus.IsValid() && cus.Exists())
            {
                Natives.SET_PED_KEEP_TASK(cus, true);
                cus.Tasks.PlayAnimation(ANIM_DICTIONARY, Main.MT.Next(2) is 0 ? ANIM_TYPE1 : ANIM_TYPE2, 1f, AnimationFlags.Loop);
                peds.Add(cus);
            }
        }
    }

    internal override void NotAccepted() { }

    internal override void Update()
    {
        if (!arrived && Game.LocalPlayer.Character.DistanceTo(citizen.Position) < 30f)
        {
            if (citizenB is not null && citizenB.IsValid() && citizenB.Exists())
            {
                citizenB.IsRouteEnabled = false;
            }
            Hud.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Citizen"), string.Empty));
            arrived = true;
        }

        if (arrived && Game.LocalPlayer.Character.DistanceTo(citizen.Position) < 4f && !Game.LocalPlayer.Character.IsInAnyVehicle(false))
        {
            KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Victim"), string.Empty], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
            if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
            {
                Dialogue.Talk(TalkToCitizen);
                Hud.DisplayHelp(Localization.GetString("DrunkCallTaxi"));
                if (citizen is not null && citizen.IsValid() && citizen.Exists()) citizen.Dismiss();
                if (citizenB is not null && citizenB.IsValid() && citizenB.Exists()) citizenB.Delete();
                End();
            }
        }
        if (KeyHelpers.IsKeysDown(Settings.Instance.EndCalloutsKey, Settings.Instance.EndCalloutsModifierKey)) End();
    }
}