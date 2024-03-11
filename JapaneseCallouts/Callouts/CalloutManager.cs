namespace JapaneseCallouts.Callouts;

internal static class CalloutManager
{
    internal static void RegisterAllCallouts()
    {
        Functions.RegisterCallout(typeof(StolenVehicle));
        // Functions.RegisterCallout(typeof(YakuzaActivity));
        Functions.RegisterCallout(typeof(BankHeist));
    }
}