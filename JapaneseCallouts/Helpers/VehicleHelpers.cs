namespace JapaneseCallouts.Helpers;

internal static class VehicleHelpers
{
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
}