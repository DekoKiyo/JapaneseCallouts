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
        var newPed = Natives.CLONE_PED(oldPed, false, false, true);
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
            var liveryCount = Natives.GET_VEHICLE_LIVERY_COUNT(vehicle);
            if (liveryCount is -1)
            {
                if (config.ColorR is >= 0 and < 256 && config.ColorG is >= 0 and < 256 && config.ColorB is >= 0 and < 256)
                {
                    Natives.SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(vehicle, config.ColorR, config.ColorG, config.ColorB);
                    Natives.SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(vehicle, config.ColorR, config.ColorG, config.ColorB);
                }
            }
            else
            {
                if (liveryCount <= config.Livery)
                {
                    Natives.SET_VEHICLE_LIVERY(vehicle, config.Livery);
                }
            }
        }
    }

    internal static void SetOutfit(this Ped ped, PedConfig data, OutfitConfig outfit)
    {
        if (CalloutHelpers.IsRandomProps(data))
        {
            ped.RandomizeVariation();
            Natives.SET_PED_RANDOM_PROPS(ped);
        }
        else
        {
            ped.GenerateRandomCharacter();
            ped.SetVariation((int)EComponents.Mask, outfit.MaskModel - 1, outfit.MaskTexture - 1);
            ped.SetVariation((int)EComponents.UpperSkin, outfit.UpperSkinModel - 1, outfit.UpperSkinTexture - 1);
            ped.SetVariation((int)EComponents.Pants, outfit.PantsModel - 1, outfit.PantsTexture - 1);
            ped.SetVariation((int)EComponents.Parachute, outfit.ParachuteModel - 1, outfit.ParachuteTexture - 1);
            ped.SetVariation((int)EComponents.Shoes, outfit.ShoesModel - 1, outfit.ShoesTexture - 1);
            ped.SetVariation((int)EComponents.Accessories, outfit.AccessoriesModel - 1, outfit.AccessoriesTexture - 1);
            ped.SetVariation((int)EComponents.Undercoat, outfit.UndercoatModel - 1, outfit.UndercoatTexture - 1);
            ped.SetVariation((int)EComponents.Armor, outfit.ArmorModel - 1, outfit.ArmorTexture - 1);
            ped.SetVariation((int)EComponents.Decal, outfit.DecalModel - 1, outfit.DecalTexture - 1);
            ped.SetVariation((int)EComponents.Top, outfit.TopModel - 1, outfit.TopTexture - 1);
            Natives.CLEAR_ALL_PED_PROPS(ped);
            Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Hat, outfit.HatModel - 1, outfit.HatTexture - 1, false);
            Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Glasses, outfit.GlassesModel - 1, outfit.GlassesTexture - 1, false);
            Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Ear, outfit.EarModel - 1, outfit.EarTexture - 1, false);
            Natives.SET_PED_PROP_INDEX(ped, (int)EProps.Watch, outfit.WatchModel - 1, outfit.WatchTexture - 1, false);
        }
    }

    internal static void GiveWeapon(this Ped ped, WeaponConfig[] weapons, bool inHand)
    {
        var weapon = CalloutHelpers.Select(weapons);
        var hash = Natives.GET_HASH_KEY(weapon.Model);
        Natives.REQUEST_MODEL(hash);
        Natives.GIVE_WEAPON_TO_PED(ped, hash, 5000, false, true);
        Natives.SET_CURRENT_PED_WEAPON(ped, hash, inHand);
        foreach (var comp in weapon.Components)
        {
            var compHash = Natives.GET_HASH_KEY(comp);
            Natives.REQUEST_MODEL(compHash);
            Natives.GIVE_WEAPON_COMPONENT_TO_PED(ped, hash, compHash);
        }
    }

    // Source: https://gtaforums.com/topic/851726-c-how-to-check-if-a-ped-is-visible/

    /// <summary>
    /// Determine if given ped is in line of sight to the player
    /// </summary>
    /// <param name="target">Ped to check for</param>
    /// <param name="source">Origin ped</param>
    /// /// <param name="minAngle">the value of the dot product at which a ped is considered not in LoS</param>
    /// <param name="withOcclusion">true if occlusion check should be included, false otherwise</param>
    /// <param name="includeDead">true if dead peds should be included, false otherwise</param>
    /// <returns>true if at least one ped was in los, false otherwise</returns>
    internal static bool IsPedInLoS(this Entity target, Entity source, float minAngle, bool withOcclusion = true, bool includeDead = false)
    {
        if (target is not null && target.IsValid() && target.Exists())
        {
            if (target.IsDead && !includeDead)
            {
                return false;
            }
            if (withOcclusion) // with obstacle detection
            {
                if (Natives.HAS_ENTITY_CLEAR_LOS_TO_ENTITY(target, source, 17)) // No Obstacles?
                {
                    var dot = GetDotVectorResult(target, source);
                    if (dot > minAngle) // Is in acceptable range for dot product?
                    {
                        return true;
                    }
                }
            }
            else // without obstacle detection
            {
                var dot = GetDotVectorResult(target, source);
                if (dot > minAngle) // Is in acceptable range for dot product?
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Determine the dot vector product between source and target ped
    /// </summary>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <returns>float in range -1.0 to 1.0, negative value if source is behind target, positive value if source is in front of target, 0 if source is orthogonal to target, 1 if directly in front of target, -1 if directly behind target</returns>
    private static float GetDotVectorResult(Entity target, Entity source)
    {
        if (target is not null && target.IsValid() && target.Exists() &&
            source is not null && source.IsValid() && source.Exists())
        {
            var dir = target.Position - source.Position;
            dir.Normalize();
            return Vector3.Dot(dir, source.ForwardVector);
        }
        else
        {
            return -1.0f;
        }
    }
}