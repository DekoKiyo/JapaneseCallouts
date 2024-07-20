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
                // var objects = World.GetAllObjects().Where(x => x.Model == new Model("hei_v_ilev_bk_gate_pris"));
                // foreach (var obj in objects)
                // {
                //     Main.Logger.Info($"X: {obj.Position.X} Y: {obj.Position.Y} Z: {obj.Position.Z}", $"{obj.Model.Hash}");
                // }
                Natives.SET_LOCKED_UNSTREAMED_IN_DOOR_OF_TYPE(4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
            }
        }
    }
}