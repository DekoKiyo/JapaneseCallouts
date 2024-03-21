namespace JapaneseCallouts.Modules;

internal static class Conversations
{
    internal const string PHONE_CALLING_SOUND = "PhoneCalling.wav";
    internal const string PHONE_BUSY_SOUND = "PhoneBusy.wav";
    internal const string SPEAKER_ANIMATION_DICTIONARY = "special_ped@jessie@monologue_1@monologue_1f";
    internal const string SPEAKER_ANIMATION_CLIP = "special_ped@jessie@jessie_ig_1_p1_heydudes555_773@monologue_1f";

    internal static bool IsTalking = false;

    internal static void Talk((string speaker, string text)[] lines, Ped ped = null)
    {
        IsTalking = true;
        var pos = Main.Player.Position;
        var heading = Main.Player.Heading;
        GameFiber.StartNew(() =>
        {
            while (IsTalking)
            {
                GameFiber.Yield();
                if (Vector3.Distance(Main.Player.Position, pos) > 2.5f)
                {
                    Main.Player.Tasks.FollowNavigationMeshToPosition(pos, heading, 1f).WaitForCompletion(1000);
                }
                if (Main.Player.IsInAnyVehicle(false))
                {
                    Main.Player.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion(1800);
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
            HudHelpers.DisplaySubtitle($"~b~{lines[i].speaker}~s~: {lines[i].text} ({i + 1}/{lines.Length})", 10000);
            while (i < lines.Length - 1)
            {
                GameFiber.Yield();
                HudHelpers.DisplayHelp(string.Format(General.PressToTalk, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~"));
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

    internal static void PlayPhoneCallingSound(int count)
    {
        PlaySoundLoop($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{PHONE_CALLING_SOUND}", count);
    }

    internal static void PlayPhoneBusySound(int count)
    {
        PlaySoundLoop($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{PHONE_BUSY_SOUND}", count);
    }

    private static void PlaySoundLoop(string path, int count)
    {
        GameFiber.StartNew(() =>
        {
            using var file = new AudioFileReader(path);
            using var output = new WaveOutEvent();
            output.Init(file);
            for (int i = 0; i < count; i++)
            {
                output.Play();
                while (output.PlaybackState is PlaybackState.Playing)
                {
                    GameFiber.Yield();
                }
                file.Position = 0;
            }
        });
    }

    internal static bool DisplayTime = false;
    internal static List<string> Answers = [];
    internal static List<Keys> AnswerKeys = [];
    internal static int DisplayAnswers(Dictionary<string, Keys> dic)
    {
        DisplayTime = true;

        Answers = [.. dic.Keys];
        AnswerKeys = [.. dic.Values];

        int answerIndex = -1;

        GameFiber.StartNew(() =>
        {
            var border = new ResRectangle(new(Game.Resolution.Width / 5 - 5, Game.Resolution.Height / 7 - 5), new(700, 180), Color.FromArgb(90, Color.Black));
            var rect = new ResRectangle(new(Game.Resolution.Width / 5, Game.Resolution.Height / 7), new(700, 180), Color.Black);
            var text = new ResText(CalloutsText.SelectAnswerText, new(border.Position.X + 100, border.Position.Y + 15), 0.4f, Color.White, Common.EFont.ChaletLondon, ResText.Alignment.Left);
            var answers = new List<ResText>();

            int YIncreaser = 45;
            for (int i = 0; i < Answers.Count; i++)
            {
                answers.Add(new($"[{AnswerKeys[i]}] {Answers[i]}", new(rect.Position.X + 10, rect.Position.Y + YIncreaser), 0.3f, Color.White, Common.EFont.ChaletLondon, ResText.Alignment.Left));
                YIncreaser += 25;
            }
            while (DisplayTime)
            {
                GameFiber.Yield();

                border.Draw();
                rect.Draw();
                text.Draw();
                foreach (var answer in answers)
                {
                    answer.Draw();
                }

                for (int i = 0; i < AnswerKeys.Count; i++)
                {
                    if (KeyHelpers.IsKeysDown(AnswerKeys[i]))
                    {
                        if (dic.Count >= i + 1)
                        {
                            answerIndex = i;
                        }
                    }
                }
            }
        });
        NativeFunction.Natives.SET_PED_CAN_SWITCH_WEAPON(Main.Player, false);
        var pos = Main.Player.Position;
        var heading = Main.Player.Heading;
        while (answerIndex is -1)
        {
            GameFiber.Yield();
            if (Vector3.Distance(Main.Player.Position, pos) > 4f)
            {
                Main.Player.Tasks.FollowNavigationMeshToPosition(pos, heading, 1.2f).WaitForCompletion(1500);
            }
            if (Main.Player.IsInAnyVehicle(false))
            {
                Main.Player.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion(1800);
            }
            if (!DisplayTime) break;
        }
        NativeFunction.Natives.SET_PED_CAN_SWITCH_WEAPON(Main.Player, true);
        DisplayTime = false;
        return answerIndex;
    }
}