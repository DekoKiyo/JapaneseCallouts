namespace JapaneseCallouts.Computers;

internal class PoliceComputer : GwenForm
{
    private RichLabel consoleTextBox;

    internal static GameFiber fiber = null;

    internal PoliceComputer() : base(typeof(PoliceComputerTemplate)) { }

    ~PoliceComputer()
    {
    }

    public override void InitializeLayout()
    {
        base.InitializeLayout();
        Window.Skin.SetDefaultFont("Segoe UI");
    }
}