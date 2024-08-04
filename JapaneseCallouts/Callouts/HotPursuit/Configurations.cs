namespace JapaneseCallouts.Callouts.HotPursuit;

internal class Configurations
{
    [JsonProperty("vehicles")]
    internal VehicleConfig[] Vehicles { get; set; }
}