namespace JapaneseCallouts.Helpers;

internal static class EntityHelpers
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

    internal static void ApplyTexture(this Vehicle vehicle, VehicleConfig config)
    {
        if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
        {
            var liveryCount = NativeFunction.Natives.GET_VEHICLE_LIVERY_COUNT<int>(vehicle);
            if (liveryCount is -1)
            {
                if (config.ColorR is >= 0 and < 256 && config.ColorG is >= 0 and < 256 && config.ColorB is >= 0 and < 256)
                {
                    NativeFunction.Natives.SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(vehicle, config.ColorR, config.ColorG, config.ColorB);
                    NativeFunction.Natives.SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(vehicle, config.ColorR, config.ColorG, config.ColorB);
                }
            }
            else
            {
                if (liveryCount <= config.Livery)
                {
                    NativeFunction.Natives.SET_VEHICLE_LIVERY(vehicle, config.Livery);
                }
            }
        }
    }

    internal static void SetOutfit(this Ped ped, PedConfig data)
    {
        if (CalloutHelpers.IsRandomProps(data))
        {
            ped.RandomizeVariation();
            NativeFunction.Natives.SET_PED_RANDOM_PROPS(ped);
        }
        else
        {
            ped.GenerateRandomCharacter();
            ped.SetVariation((int)EComponents.Mask, data.MaskModel - 1, data.MaskTexture - 1);
            ped.SetVariation((int)EComponents.UpperSkin, data.UpperSkinModel - 1, data.UpperSkinTexture - 1);
            ped.SetVariation((int)EComponents.Pants, data.PantsModel - 1, data.PantsTexture - 1);
            ped.SetVariation((int)EComponents.Parachute, data.ParachuteModel - 1, data.ParachuteTexture - 1);
            ped.SetVariation((int)EComponents.Shoes, data.ShoesModel - 1, data.ShoesTexture - 1);
            ped.SetVariation((int)EComponents.Accessories, data.AccessoriesModel - 1, data.AccessoriesTexture - 1);
            ped.SetVariation((int)EComponents.Undercoat, data.UndercoatModel - 1, data.UndercoatTexture - 1);
            ped.SetVariation((int)EComponents.Armor, data.ArmorModel - 1, data.ArmorTexture - 1);
            ped.SetVariation((int)EComponents.Decal, data.DecalModel - 1, data.DecalTexture - 1);
            ped.SetVariation((int)EComponents.Top, data.TopModel - 1, data.TopTexture - 1);
            NativeFunction.Natives.CLEAR_ALL_PED_PROPS(ped);
            NativeFunction.Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Hat, data.HatModel - 1, data.HatTexture - 1, false);
            NativeFunction.Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Glasses, data.GlassesModel - 1, data.GlassesTexture - 1, false);
            NativeFunction.Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Ear, data.EarModel - 1, data.EarTexture - 1, false);
            NativeFunction.Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Watch, data.WatchModel - 1, data.WatchTexture - 1, false);
        }
    }
}