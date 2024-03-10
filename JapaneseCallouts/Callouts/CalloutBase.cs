using LSPD_First_Response.Engine;

namespace JapaneseCallouts.Callouts;

internal abstract class CalloutBase : Callout
{
    internal bool IsCalloutRunning { get; private set; } = false;
    internal abstract void Setup();
    internal abstract void OnDisplayed();
    internal abstract void Accepted();
    internal abstract void NotAccepted();
    internal abstract void Update();
    internal delegate void EndCallouts();
    internal event EndCallouts OnCalloutsEnded;
    internal virtual List<Entity> DeleteEntities() { return []; }

    public override bool OnBeforeCalloutDisplayed()
    {
        Setup();
        return base.OnBeforeCalloutDisplayed();
    }

    public override void OnCalloutDisplayed()
    {
        OnDisplayed();
        base.OnCalloutDisplayed();
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
        foreach (var entity in DeleteEntities())
        {
            if (entity is not null && entity.IsValid() && entity.Exists())
            {
                entity.Dismiss();
                entity.Delete();
            }
        }
        base.OnCalloutNotAccepted();
    }

    public override void End()
    {
        OnCalloutsEnded?.Invoke();
        IsCalloutRunning = false;
        Functions.PlayScannerAudio("ATTENTION_ALL_UNITS JP_WE_ARE_CODE JP_FOUR JP_NO_FURTHER_UNITS_REQUIRED");
        base.End();
    }
}