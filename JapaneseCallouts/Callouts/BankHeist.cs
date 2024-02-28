
namespace JapaneseCallouts.Callouts;

internal class BankHeist : CalloutBase
{
    private const string ALARM_SOUND_FILE_NAME = "BankHeistAlarm.wav";

    private static SoundPlayer BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{ALARM_SOUND_FILE_NAME}");

    internal override void Setup()
    {
    }

    internal override void OnDisplayed()
    {
    }

    internal override void Accepted()
    {
    }

    internal override void Update()
    {
    }

    internal override void EndCallout(bool notAccepted = false, bool isPlayerDead = false)
    {
    }
}