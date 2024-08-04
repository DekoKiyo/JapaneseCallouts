namespace JapaneseCallouts.Callouts.RoadRage;

internal class Configurations : IConfig
{
    [JsonPropertyName("victim_vehicles")]
    internal VehicleConfig[] VictimVehicles { get; set; }
    [JsonPropertyName("suspect_vehicles")]
    internal VehicleConfig[] SuspectVehicles { get; set; }
    [JsonPropertyName("victim_peds")]
    internal PedConfig[] VictimPeds { get; set; }
    [JsonPropertyName("suspect_peds")]
    internal PedConfig[] SuspectPeds { get; set; }
}