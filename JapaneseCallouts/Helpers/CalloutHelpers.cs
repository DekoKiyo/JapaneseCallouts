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
}