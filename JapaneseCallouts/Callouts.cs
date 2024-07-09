namespace JapaneseCallouts;

internal abstract class CalloutBase : Callout
{
    internal bool IsCalloutRunning { get; private set; } = false;
    internal bool NoLastRadio { private get; set; } = false;
    internal abstract void Setup();
    internal abstract void OnDisplayed();
    internal abstract void Accepted();
    internal abstract void NotAccepted();
    internal abstract void Update();
    internal delegate void EndCallouts();
    internal event EndCallouts OnCalloutsEnded;

    public override bool OnBeforeCalloutDisplayed()
    {
        Setup();
        return base.OnBeforeCalloutDisplayed();
    }

    public override void OnCalloutDisplayed()
    {
        OnDisplayed();
        base.OnCalloutDisplayed();
        NativeFunction.Natives.GET_STREET_NAME_AT_COORD(CalloutPosition.X, CalloutPosition.Y, CalloutPosition.Z, out uint hash, out uint _);
        CalloutHelpers.DisplayTranslatedNotification(CalloutMessage, NativeFunction.Natives.GET_STREET_NAME_FROM_HASH_KEY<string>(hash));
    }

    public override bool OnCalloutAccepted()
    {
        Accepted();
        IsCalloutRunning = true;
        return base.OnCalloutAccepted();
    }

    public override void Process()
    {
        Update();
        base.Process();
    }

    public override void OnCalloutNotAccepted()
    {
        IsCalloutRunning = false;
        NotAccepted();
        base.OnCalloutNotAccepted();
    }

    public override void End()
    {
        base.End();
        IsCalloutRunning = false;
        OnCalloutsEnded?.Invoke();
        if (!NoLastRadio) Functions.PlayScannerAudio("ATTENTION_ALL_UNITS JP_WE_ARE_CODE JP_FOUR JP_NO_FURTHER_UNITS_REQUIRED");
    }

    internal static void RegisterAllCallouts()
    {
        Functions.RegisterCallout(typeof(BankHeist));
        Functions.RegisterCallout(typeof(PacificBankHeistN));
        Functions.RegisterCallout(typeof(DrunkGuys));
        Functions.RegisterCallout(typeof(HotPursuit));
        Functions.RegisterCallout(typeof(StolenVehicle));
        Functions.RegisterCallout(typeof(StoreRobbery));
        Functions.RegisterCallout(typeof(RoadRage));
        Functions.RegisterCallout(typeof(StreetFight));
        Functions.RegisterCallout(typeof(WantedCriminalFound));
        // Functions.RegisterCallout(typeof(YakuzaActivity));
    }
}