namespace JapaneseCallouts.Modules;

internal class Question
{
    private bool IsShown = false;

    private ResRectangle Background;
    private ResRectangle Border;
    private ResText QuestionTitle;
    private readonly List<ResText> AnswersTexts = [];

    internal Question() { }

    internal int Show(string question, (Keys key, string text)[] answers, bool pauseGame = false)
    {
        Background = new(new(Game.Resolution.Width / 5, Game.Resolution.Height / 7), new(700, 180), Color.Black);
        Border = new(new(Game.Resolution.Width / 5 - 5, Game.Resolution.Height / 7 - 5), new(700, 180), Color.FromArgb(90, Color.Black));
        QuestionTitle = new(question, new(Border.Position.X + 20, Border.Position.Y + 15), 0.4f, Color.White, Common.EFont.ChaletLondon, ResText.Alignment.Left);

        var height = 45;
        for (int i = 0; i < answers.Length; i++)
        {
            var text = new ResText($"[{answers[i].key.ToString()}] {answers[i].text}", new(Background.Position.X + 10, Background.Position.Y + height), 0.3f, Color.White, Common.EFont.ChaletLondon, ResText.Alignment.Left);
            AnswersTexts.Add(text);
            height += 25;
        }
        Natives.SET_PED_CAN_SWITCH_WEAPON(Game.LocalPlayer.Character, false);

        var index = 0;
        IsShown = true;
        GameFiber.StartNew(() =>
        {
            while (IsShown)
            {
                GameFiber.Yield();
                if (pauseGame && !Game.IsPaused) Game.IsPaused = true;

                Background.Draw();
                Border.Draw();
                QuestionTitle.Draw();
                foreach (var text in AnswersTexts) text.Draw();

                for (int i = 0; i < answers.Length; i++)
                {
                    if (KeyHelpers.IsKeysDown(answers[i].key))
                    {
                        index = i;
                    }
                }
            }
        });
        GameFiber.WaitUntil(() => index is not -1 || !IsShown);
        Natives.SET_PED_CAN_SWITCH_WEAPON(Game.LocalPlayer.Character, true);
        if (pauseGame) Game.IsPaused = false;
        IsShown = false;

        return index;
    }
}