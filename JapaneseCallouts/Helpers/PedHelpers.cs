namespace JapaneseCallouts.Helpers;

internal static class PedHelpers
{
    internal static Ped ClonePed(this Ped oldPed)
    {
        var oldPos = oldPed.Position;
        float oldHeading = oldPed.Heading;
        bool spawnInVehicle = false;
        Vehicle car = null;
        int seatIndex = 0;
        int oldArmor = oldPed.Armor;
        int oldHealth = oldPed.Health;
        if (oldPed.IsInAnyVehicle(false))
        {
            car = oldPed.CurrentVehicle;
            seatIndex = oldPed.SeatIndex;
            spawnInVehicle = true;
        }
        var newPed = NativeFunction.Natives.ClonePed<Ped>(oldPed, oldPed.Heading, false, true);
        if (oldPed.Exists() && oldPed.IsValid())
        {
            oldPed.Delete();
        }
        newPed.Position = oldPos;
        newPed.Heading = oldHeading;

        if (spawnInVehicle)
        {
            newPed.WarpIntoVehicle(car, seatIndex);
        }
        newPed.Health = oldHealth;
        newPed.Armor = oldArmor;
        newPed.BlockPermanentEvents = true;
        newPed.IsPersistent = true;
        return newPed;
    }

    internal static bool IsAllPedDeadOrArrested(Ped[] peds)
    {
        var total = peds.Length;
        var count = 0;
        foreach (var ped in peds)
        {
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                if (ped.IsDead || Functions.IsPedArrested(ped)) count++;
            }
        }

        return count == total;
    }
}