namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Drunk Guys", CalloutProbability.High)]
internal class DrunkGuys : CalloutBase
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
        (Settings.OfficerName, Localization.GetString("DrunkCitizen1")),
        (Localization.GetString("Citizen"), Localization.GetString("DrunkCitizen2")),
        (Localization.GetString("Citizen"), Localization.GetString("DrunkCitizen3")),
        (Settings.OfficerName, Localization.GetString("DrunkCitizen4")),
        (Localization.GetString("Citizen"), Localization.GetString("DrunkCitizen5")),
    ];

    internal override void Setup()
    {
        var positions = new List<Vector3>();
        foreach (var p in XmlManager.DrunkGuysConfig.DrunkGuysPositions)
        {
            positions.Add(new(p.X, p.Y, p.Z));
        }
        var index = Vector3Helpers.GetNearestPosIndex(positions);
        calloutData = XmlManager.DrunkGuysConfig.DrunkGuysPositions[index];
        peds = new(calloutData.DrunkPos.Count);
        CalloutPosition = positions[index];
        CalloutMessage = Localization.GetString("DrunkGuys");
        NoLastRadio = true;
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 20f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.DrunkGuys, CalloutPosition);

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
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
            NativeFunction.Natives.SET_PED_KEEP_TASK(citizen, true);

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
                NativeFunction.Natives.SET_PED_KEEP_TASK(cus, true);
                cus.Tasks.PlayAnimation(ANIM_DICTIONARY, Main.MersenneTwister.Next(2) is 0 ? ANIM_TYPE1 : ANIM_TYPE2, 1f, AnimationFlags.Loop);
                peds.Add(cus);
            }
        }
    }

    internal override void NotAccepted() { }

    internal override void Update()
    {
        if (!arrived && Main.Player.DistanceTo(citizen.Position) < 30f)
        {
            if (citizenB is not null && citizenB.IsValid() && citizenB.Exists())
            {
                citizenB.IsRouteEnabled = false;
            }
            Hud.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Citizen"), string.Empty));
            arrived = true;
        }

        if (arrived && Main.Player.DistanceTo(citizen.Position) < 4f && !Main.Player.IsInAnyVehicle(false))
        {
            KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Victim"), string.Empty], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
            {
                Conversations.Talk(TalkToCitizen);
                Hud.DisplayHelp(Localization.GetString("DrunkCallTaxi"));
                if (citizen is not null && citizen.IsValid() && citizen.Exists()) citizen.Dismiss();
                if (citizenB is not null && citizenB.IsValid() && citizenB.Exists()) citizenB.Delete();
                End();
            }
        }
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }
}