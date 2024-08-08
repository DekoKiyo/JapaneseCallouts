namespace JapaneseCallouts.Modules;

// メモ: MSゴシックだと半角:全角=1:2
// だけどMSゴシックは日本向けフォントだよね

internal static class Dialogue
{
    internal static void Talk((string speaker, string text)[] lines, bool displayCount = true, Ped target = null)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            var sb = new StringBuilder("~b~").Append(lines[i].speaker).Append("~s~: ").Append(lines[i].text);
            if (displayCount)
            {
                sb.Append(" (").Append((i + 1).ToString()).Append("/").Append(lines.Length.ToString()).Append(")");
            }
            Hud.DisplaySubtitle(sb.ToString(), 12000);
            while (i < lines.Length - 1)
            {
                GameFiber.Yield();
                if (target is not null && target.IsValid() && target.Exists())
                {
                    if (Vector3.Distance(target.Position, Game.LocalPlayer.Character.Position) > 5f)
                    {
                        // TODO
                        Hud.DisplayHelp(Localization.GetString(""));
                    }
                    else
                    {
                        KeyHelpers.DisplayKeyHelp("PressToTalk", Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                        if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey)) break;
                    }
                }
            }
        }
        Game.HideHelp();
    }
}