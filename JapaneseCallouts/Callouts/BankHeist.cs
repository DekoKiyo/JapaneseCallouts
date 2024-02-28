
namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] BankHeist", CalloutProbability.Low)]
internal class BankHeist : CalloutBase
{
    private const string ALARM_SOUND_FILE_NAME = "BankHeistAlarm.wav";

    private static SoundPlayer BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{ALARM_SOUND_FILE_NAME}");

    private Vector3 BankLocation = new();

    private List<Ped> Robbers = [];
    private List<Ped> Hostages = [];

    private int HostageCount = 0;
    private int AlivedHostageCount = 0;
    private int DiedHostageCount = 0;
    private int RemainningRobberCount = 0;
    private int DiedRobberCount = 0;

    private bool EnableAlarmSound = true;
    private bool IsFightingBegan = false;

    internal override void Setup()
    {
        CalloutPosition = BankLocation;
        CalloutMessage = Localization.GetString("BankHeist");
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 12f);
        Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99", CalloutPosition);

        if (Main.IsCalloutInterfaceAPIExist)
        {
            CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("BankHeistDesc"));
        }
    }

    internal override void OnDisplayed()
    {
    }

    internal override void Accepted()
    {
        HudExtensions.DisplayNotification(Localization.GetString("BankHeistDesc"));
    }

    internal override void Update()
    {
    }

    internal override void EndCallout(bool notAccepted = false, bool isPlayerDead = false)
    {
    }
}