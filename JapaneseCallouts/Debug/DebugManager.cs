namespace JapaneseCallouts.Debug;

internal static class DebugManager
{
    internal static void Initialize()
    {
        GameFiber.StartNew(Update);
    }

    internal static void Update()
    {
        while (true)
        {
            GameFiber.Yield();
            if (KeyHelpers.IsKeysDown(Keys.O))
            {
                var objects = World.GetAllObjects().Where(x => x.Model == new Model("v_ilev_genbankdoor1") || x.Model == new Model("v_ilev_genbankdoor2"));
                foreach (var obj in objects)
                {
                    Logger.Info($"X: {obj.Position.X} Y: {obj.Position.Y} Z: {obj.Position.Z}", $"{obj.Model.Hash}");
                }
            }
        }
    }
}