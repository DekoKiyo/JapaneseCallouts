namespace JapaneseCallouts.Helpers;

internal static class CalloutHelpers
{
    internal static void DisplayTranslatedNotification(string callName, string location)
    {
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalMilliseconds < 100)
        {
            GameFiber.Yield();
            NativeFunction.Natives.THEFEED_FLUSH_QUEUE();
            NativeFunction.Natives.THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(true);
            NativeFunction.Natives.THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(false);
        }
        HudHelpers.DisplayNotification(Localization.GetString("CalloutNotificationText", callName, location), Localization.GetString("Dispatch"), Localization.GetString("CalloutNotificationSubtitle"), "CHAR_CALL911", "CHAR_CALL911");
    }

    internal static T Select<T>(T[] data) where T : IBackupObject
    {
        int total = 0, pick = 0;
        foreach (var l in data) total += l.Chance;

        int rnd = Main.MersenneTwister.Next(total);
        for (int i = 0; i < data.Length; i++)
        {
            if (rnd < data[i].Chance)
            {
                pick = i;
                break;
            }
            rnd -= data[i].Chance;
        }

        return data[pick];
    }

    internal static PedConfig SelectPed(EWeather weather, PedConfig[] data)
    {
        var list = data.Where(x => weather is EWeather.Rainy ? x.IsRainy : weather is EWeather.Snowy ? x.IsSnowy : x.IsSunny).ToArray();
        return list.Length is 0 ? Select(data) : Select(list);
    }

    internal static EWeather GetWeatherType(EWeatherType weatherType)
    {
        return weatherType switch
        {
            EWeatherType.Blizzard or EWeatherType.Christmas or EWeatherType.Snow or EWeatherType.LightSnow => EWeather.Snowy,
            EWeatherType.Rain or EWeatherType.Thunder or EWeatherType.Clearing => EWeather.Rainy,
            _ => EWeather.Sunny
        };
    }

    internal static bool IsRandomProps(PedConfig preset)
    {
        return preset.RandomProps ||
            (preset.MaskModel is 0 &&
            preset.UpperSkinModel is 0 &&
            preset.PantsModel is 0 &&
            preset.ParachuteModel is 0 &&
            preset.ShoesModel is 0 &&
            preset.AccessoriesModel is 0 &&
            preset.UndercoatModel is 0 &&
            preset.ArmorModel is 0 &&
            preset.DecalModel is 0 &&
            preset.TopModel is 0 &&
            preset.HatModel is 0 &&
            preset.GlassesModel is 0 &&
            preset.EarModel is 0 &&
            preset.WatchModel is 0);
    }

    internal static void SetOutfit(this Ped ped, PedConfig data)
    {
        if (IsRandomProps(data))
        {
            ped.RandomizeVariation();
            NativeFunction.Natives.SET_PED_RANDOM_PROPS(ped);
        }
        else
        {
            ped.GenerateRandomFace();
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