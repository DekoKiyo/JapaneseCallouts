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
                // Natives.SET_LOCKED_UNSTREAMED_IN_DOOR_OF_TYPE(4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                TestCAM();
            }
        }
    }

    private static void TestCAM()
    {
        var ped = new Ped(Game.LocalPlayer.Character.GetOffsetPositionFront(5f));
        if (ped is not null && ped.IsValid() && ped.Exists())
        {
            var cCam = Natives.GET_RENDERING_CAM();
            Natives.SET_CAM_ACTIVE(cCam, false);
            var offset = ped.GetOffsetPositionFront(5f);
            var cam = Natives.CREATE_CAM_WITH_PARAMS("DEFAULT_SCRIPTED_CAMERA", offset.X, offset.Y, offset.Z, ped.Heading, 0f, 0f, 0f, false, 2);
            Natives.ATTACH_CAM_TO_ENTITY(cam, Game.LocalPlayer.Character, 0f, 0f, 0f, false);
            Natives.SET_CAM_ACTIVE(cam, true);
        }
    }
}