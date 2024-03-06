namespace JapaneseCallouts.Modules;

internal static class Conversations
{
    internal const string SPEAKER_ANIMATION_DICTIONARY = "special_ped@jessie@monologue_1@monologue_1f";
    internal const string SPEAKER_ANIMATION_CLIP = "special_ped@jessie@jessie_ig_1_p1_heydudes555_773@monologue_1f";

    internal static bool IsTalking = false;
    internal static void Talk((string speaker, string text)[] lines, Ped ped = null)
    {
        IsTalking = true;
        var pos = Game.LocalPlayer.Character.Position;
        var heading = Game.LocalPlayer.Character.Heading;
        GameFiber.StartNew(() =>
        {
            while (IsTalking)
            {
                GameFiber.Yield();
                if (Vector3.Distance(Game.LocalPlayer.Character.Position, pos) > 2.5f)
                {
                    Game.LocalPlayer.Character.Tasks.FollowNavigationMeshToPosition(pos, heading, 1f).WaitForCompletion(1000);
                }
                if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
                {
                    Game.LocalPlayer.Character.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion(1800);
                }
            }
        });
        if (ped is not null)
        {
            if (ped.IsValid() && ped.Exists())
            {
                if (!ped.IsInAnyVehicle(false))
                {
                    ped.Tasks.PlayAnimation(SPEAKER_ANIMATION_DICTIONARY, SPEAKER_ANIMATION_CLIP, 1f, AnimationFlags.Loop);
                }
            }
        }
        for (int i = 0; i < lines.Length; i++)
        {
            HudHelpers.DisplaySubtitle($"~b~{lines[i].speaker}~s~: {lines[i].text} ({i}/{lines.Length})", 10000);
            while (i < lines.Length - 1)
            {
                GameFiber.Yield();

                if (Settings.SpeakWithThePersonModifierKey is Keys.None) HudHelpers.DisplayNotification(string.Format(General.PressToTalkWith, Settings.SpeakWithThePersonKey.GetInstructionalId(), CalloutsText.Commander));
                else HudHelpers.DisplayNotification(string.Format(General.PressToTalkWith, $"{Settings.SpeakWithThePersonKey.GetInstructionalId()} ~+~ {Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}", CalloutsText.Commander));

                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey)) break;
            }
            if (!IsTalking) break;
        }
        IsTalking = false;
        if (ped is not null)
        {
            if (ped.IsValid() && ped.Exists())
            {
                ped.Tasks.ClearImmediately();
            }
        }
        Game.HideHelp();
    }
}