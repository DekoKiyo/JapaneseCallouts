namespace JapaneseCallouts.Computers;

internal class PoliceComputer : GwenForm
{
    private GComboBox langComboBox;
    private GLabel langLabel;

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