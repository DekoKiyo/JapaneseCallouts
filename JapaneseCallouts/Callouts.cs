using JapaneseCallouts.Callouts.BankHeist;
using JapaneseCallouts.Callouts.DrunkGuys;
using JapaneseCallouts.Callouts.HotPursuit;
using JapaneseCallouts.Callouts.PacificBankHeist;
using JapaneseCallouts.Callouts.RoadRage;
using JapaneseCallouts.Callouts.StoreRobbery;
using JapaneseCallouts.Callouts.StreetFight;
using JapaneseCallouts.Callouts.WantedCriminalFound;

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
        Natives.GET_STREET_NAME_AT_COORD(CalloutPosition.X, CalloutPosition.Y, CalloutPosition.Z, out uint hash, out uint _);
        CalloutHelpers.DisplayTranslatedNotification(CalloutMessage, Natives.GET_STREET_NAME_FROM_HASH_KEY(hash));
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
        RegisterCallout<BankHeist, Callouts.BankHeist.Configurations>(Settings.Instance.EnableBankHeist);
        RegisterCallout<PacificBankHeist, Callouts.PacificBankHeist.Configurations>(Settings.Instance.EnablePacificBankHeist);
        RegisterCallout<DrunkGuys, Callouts.DrunkGuys.Configurations>(Settings.Instance.EnableDrunkGuys);
        RegisterCallout<RoadRage, Callouts.RoadRage.Configurations>(Settings.Instance.EnableRoadRage);
        RegisterCallout<StolenVehicle>(Settings.Instance.EnableStolenVehicle);
        RegisterCallout<StoreRobbery, Callouts.StoreRobbery.Configurations>(Settings.Instance.EnableStoreRobbery);
        RegisterCallout<HotPursuit, Callouts.HotPursuit.Configurations>(Settings.Instance.EnableHotPursuit);
        RegisterCallout<WantedCriminalFound, Callouts.WantedCriminalFound.Configurations>(Settings.Instance.EnableWantedCriminalFound);
        RegisterCallout<StreetFight, Callouts.StreetFight.Configurations>(Settings.Instance.EnableStreetFight);
    }

    private static void RegisterCallout<T>(bool enabled) where T : CalloutBase
    {
        if (enabled)
        {
            Functions.RegisterCallout(typeof(T));
            Main.Logger.Info($"The callout {nameof(T)} was registered.");
        }
        else
        {
            Main.Logger.Info($"The callout {nameof(T)} was not registered. The callout was skipped loading with setting.");
        }
    }

    private static void RegisterCallout<T, T2>(bool enabled) where T : CalloutBase<T2> where T2 : IConfig
    {
        if (enabled)
        {
            Functions.RegisterCallout(typeof(T));
            Main.Logger.Info($"The callout {nameof(T)} was registered.");
        }
        else
        {
            Main.Logger.Info($"The callout {nameof(T)} was not registered. The callout was skipped loading with setting.");
        }
    }
}

internal abstract class CalloutBase<T> : CalloutBase where T : IConfig
{
    internal static T Configuration;
}