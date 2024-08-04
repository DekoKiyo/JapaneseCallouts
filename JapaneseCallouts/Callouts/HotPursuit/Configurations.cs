namespace JapaneseCallouts.Callouts.HotPursuit;

internal class Configurations
{
    [JsonPropertyName("vehicles")]
    internal VehicleConfig[] Vehicles { get; set; }
}