namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Drunk Guys", CalloutProbability.High)]
internal class DrunkGuys : CalloutBase
{
    private Ped citizen;
    private Blip citizenB;
    private readonly List<Ped> peds = [];
    private bool arrived = false, talked = false;
    private const string CLIP_SET = "MOVE_M@DRUNK@VERYDRUNK";
    private const string ANIM_DICTIONARY = "missarmenian2";
    private const string ANIM_TYPE1 = "corpse_search_exit_ped";
    private const string ANIM_TYPE2 = "drunk_loop";
    private readonly Dictionary<Vector3, (float citizenHeading, (Vector3 pos, float heading)[] customers)> positions = new()
    {
        {
            new(118.3f, -1313.6f, 29.1f),
            (-142.6f,
                [
                    (new(120.9f, -1310.5f, 29.2f), -2.6f),
                    (new(125.5f, -1309.6f, 29.1f), 93.9f),
                    (new(128.6f, -1308.5f, 29.1f), 93.8f),
                ]
            )
        },
        {
            new(-559.8f, 273.6f, 83.0f),
            (176.7f,
                [
                    (new(-562.3f, 273.6f, 83.0f), -17.4f),
                    (new(-565.5f, 272.5f, 83.0f), 16.2f),
                    (new(-569.3f, 274.2f, 82.9f), 36.8f),
                ]
            )
        },
        {
            new(223.0f, 340.1f, 105.6f),
            (-16.4f,
                [
                    (new(208.1f, 349.3f, 105.7f), 115.5f),
                    (new(217.3f, 346.1f, 105.5f), 97.3f),
                    (new(223.3f, 344.1f, 105.5f), -56.6f),
                    (new(227.5f, 345.3f, 106.7f), -98.4f),
                ]
            )
        },
        {
            new(-301.9f, 6254.2f, 31.5f),
            (-141.1f,
                [
                    (new(-291.5f, 6257.1f, 31.4f), -155.6f),
                    (new(-295.8f, 6256.6f, 31.4f), 8.6f),
                    (new(-302.2f, 6249.8f, 31.4f), 30.8f),
                    (new(-307.1f, 6244.9f, 31.2f), 89.8f),
                ]
            )
        }
    };

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
        var pos = Vector3Helpers.GetNearestPos([.. positions.Keys]);
        CalloutPosition = pos;
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
            }
            else
            {
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("DrunkGuys"));
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        citizen = new(CalloutPosition, positions[CalloutPosition].citizenHeading)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
            KeepTasks = true,
        };
        if (citizen is not null && citizen.IsValid() && citizen.Exists())
        {
            citizenB = citizen.AttachBlip();
            citizenB.Color = Color.Green;
            citizenB.IsRouteEnabled = true;
        }
        foreach (var (pos, heading) in positions[CalloutPosition].customers)
        {
            var cus = new Ped(x => x.IsPed, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                KeepTasks = true,
                MovementAnimationSet = CLIP_SET
            };
            if (cus is not null && cus.IsValid() && cus.Exists())
            {
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
            HudHelpers.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Citizen")));
            arrived = true;
        }

        if (arrived && !talked && Main.Player.DistanceTo(citizen.Position) < 4f && !Main.Player.IsInAnyVehicle(false))
        {
            KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Victim")], Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
            {
                Conversations.Talk(TalkToCitizen);
                HudHelpers.DisplayHelp(Localization.GetString("DrunkCallTaxi"));
                if (citizen is not null && citizen.IsValid() && citizen.Exists()) citizen.Dismiss();
                if (citizenB is not null && citizenB.IsValid() && citizenB.Exists()) citizenB.Delete();
                End();
            }
        }
    }
}