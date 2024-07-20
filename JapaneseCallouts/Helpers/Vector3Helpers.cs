namespace JapaneseCallouts.Helpers;

internal static class Vector3Helpers
{
    internal static Vector3 GetNearestPos(this List<Vector3> list)
    {
        var closest = list[0];
        var distance = Vector3.Distance(Game.LocalPlayer.Character.Position, list[0]);
        for (int i = 1; i < list.Count; i++)
        {
            if (Vector3.Distance(Game.LocalPlayer.Character.Position, list[i]) <= distance)
            {
                distance = Vector3.Distance(Game.LocalPlayer.Character.Position, list[i]);
                closest = list[i];
            }
        }
        return closest;
    }

    internal static int GetNearestPosIndex(this List<Vector3> list)
    {
        var closest = list[0];
        var distance = Vector3.Distance(Game.LocalPlayer.Character.Position, list[0]);
        for (int i = 1; i < list.Count; i++)
        {
            if (Vector3.Distance(Game.LocalPlayer.Character.Position, list[i]) <= distance)
            {
                distance = Vector3.Distance(Game.LocalPlayer.Character.Position, list[i]);
                closest = list[i];
            }
        }
        return list.IndexOf(closest);
    }
}