namespace JapaneseCallouts.Modules;

internal class Door
{
    internal Vector3 Location { get; }
    internal uint ModelHash { get; }

    internal Door(Vector3 Location, uint ModelHash)
    {
        this.Location = Location;
        this.ModelHash = ModelHash;
    }
}