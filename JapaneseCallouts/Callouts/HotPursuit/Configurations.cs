namespace JapaneseCallouts.Callouts.HotPursuit;

internal class Configurations : IConfig
{
    [JsonPropertyName("vehicles")]
    internal VehicleConfig[] Vehicles { get; set; }
}