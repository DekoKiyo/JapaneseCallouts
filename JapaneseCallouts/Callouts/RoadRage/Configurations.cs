namespace JapaneseCallouts.Callouts.RoadRage;

internal class RoadRageConfig
{
    internal VehicleConfig[] VictimVehicles { get; set; }
    internal VehicleConfig[] SuspectVehicles { get; set; }
    internal PedConfig[] VictimPeds { get; set; }
    internal PedConfig[] SuspectPeds { get; set; }
}