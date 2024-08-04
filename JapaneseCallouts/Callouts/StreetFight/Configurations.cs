namespace JapaneseCallouts.Callouts.StreetFight;

internal class Configurations
{
    [JsonProperty("suspects")]
    internal PedConfig[] Suspects { get; set; }
}