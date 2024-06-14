using JapaneseCallouts.Computer.HackedPoliceComputer;

namespace JapaneseCallouts.Computers;

internal class PoliceComputer : GwenForm
{
    private GButton button1;

    internal static GameFiber fiber = null;

    internal PoliceComputer() : base(typeof(PoliceComputerTemplate)) { }

    ~PoliceComputer()
    {
        button1.Clicked -= TempButtonClicked;
    }

    public override void InitializeLayout()
    {
        base.InitializeLayout();
        Window.Skin.SetDefaultFont("Microsoft Sans Serif");
        button1.Clicked += TempButtonClicked;
    }

    private void TempButtonClicked(Base sender, ClickedEventArgs e)
    {
        Logger.Info("Clicked!");
    }
}