namespace JapaneseCallouts.Helpers;

internal static class GameFiberHelpers
{
    internal static void Resume(this GameFiber fiber)
    {
        if (fiber != null && !fiber.IsAlive)
        {
            if (fiber.IsHibernating) fiber.Wake();
            else fiber.Start();
        }
    }
    internal static bool IsRunning(this GameFiber fiber)
    {
        return fiber.IsAlive && !fiber.IsHibernating;
    }
}