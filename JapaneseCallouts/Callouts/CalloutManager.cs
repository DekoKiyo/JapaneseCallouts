namespace JapaneseCallouts.Callouts;

internal static class CalloutManager
{
    internal static void RegisterAllCallouts()
    {
        Functions.RegisterCallout(typeof(BankHeist));
        Functions.RegisterCallout(typeof(PacificBankHeist));
        Functions.RegisterCallout(typeof(DrunkGuys));
        Functions.RegisterCallout(typeof(HotPursuit));
        Functions.RegisterCallout(typeof(StolenVehicle));
        Functions.RegisterCallout(typeof(StoreRobbery));
        Functions.RegisterCallout(typeof(RoadRage));
        // Functions.RegisterCallout(typeof(YakuzaActivity));
    }
}