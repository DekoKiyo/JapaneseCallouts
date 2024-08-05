namespace JapaneseCallouts.Helpers;

internal static class CalloutHelpers
{
    internal static void DisplayTranslatedNotification(string callName, string location)
    {
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalMilliseconds < 100)
        {
            GameFiber.Yield();
            Natives.THEFEED_FLUSH_QUEUE();
            Natives.THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(true);
            Natives.THEFEED_SET_SNAP_FEED_ITEM_POSITIONS(false);
        }
        Hud.DisplayNotification(Localization.GetString("CalloutNotificationText", callName, location), Localization.GetString("Dispatch"), Localization.GetString("CalloutNotificationSubtitle"), "CHAR_CALL911", "CHAR_CALL911");
    }

    internal static T Select<T>(T[] data) where T : IChanceObject
    {
        int total = 0, pick = 0;
        foreach (var l in data) total += l.Chance;

        int rnd = Main.MT.Next(total);
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
        return ConfigurationManager.GetOutfit(preset, out OutfitConfig outfit)
            ? preset.RandomProps ||
                (outfit.MaskModel is 0 &&
                outfit.UpperSkinModel is 0 &&
                outfit.PantsModel is 0 &&
                outfit.ParachuteModel is 0 &&
                outfit.ShoesModel is 0 &&
                outfit.AccessoriesModel is 0 &&
                outfit.UndercoatModel is 0 &&
                outfit.ArmorModel is 0 &&
                outfit.DecalModel is 0 &&
                outfit.TopModel is 0 &&
                outfit.HatModel is 0 &&
                outfit.GlassesModel is 0 &&
                outfit.EarModel is 0 &&
                outfit.WatchModel is 0)
            : preset.RandomProps;
    }
}