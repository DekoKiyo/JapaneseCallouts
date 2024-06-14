namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Hacked Police Computer", CalloutProbability.High)]
internal class HackedPoliceComputer : CalloutBase
{
    private int index = 0;

    internal override void Setup()
    {
        var list = new List<Vector3>();
        foreach (var cpu in XmlManager.HackedPoliceComputerConfig.Computers)
        {
            list.Add(new(cpu.X, cpu.Y, cpu.Z));
        }
        index = list.GetNearestPosIndex();

        CalloutMessage = Localization.GetString("HackedPoliceComputer");
        CalloutPosition = new(XmlManager.HackedPoliceComputerConfig.Computers[index].X, XmlManager.HackedPoliceComputerConfig.Computers[index].Y, XmlManager.HackedPoliceComputerConfig.Computers[index].Z);
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 70f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.HackedPoliceComputer, CalloutPosition);

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
            {
            }
            else
            {
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("HackedPoliceComputer"));
            }
        };
    }

    internal override void Accepted()
    {
    }

    internal override void NotAccepted()
    {
    }

    internal override void OnDisplayed()
    {
    }

    internal override void Update()
    {
    }
}