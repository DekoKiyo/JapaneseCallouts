namespace JapaneseCallouts.Callouts;

internal abstract class CalloutBase : Callout
{
    internal bool IsCalloutRunning { get; private set; } = false;
    internal abstract void Setup();
    internal abstract void OnDisplayed();
    internal abstract void Accepted();
    internal abstract void Update();
    internal abstract void EndCallout(bool notAccepted = false, bool isPlayerDead = false);
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
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                Update();
            }
        }, $"[{Main.PLUGIN_NAME}] Callout Update Process");
        return base.OnCalloutAccepted();
    }

    public override void OnCalloutNotAccepted()
    {
        EndCallout(true);
        IsCalloutRunning = false;
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
        IsCalloutRunning = false;
        EndCallout(false);
        base.End();
    }
}