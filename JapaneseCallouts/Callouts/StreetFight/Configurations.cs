namespace JapaneseCallouts.Callouts.StreetFight;

internal class Configurations
{
    [JsonPropertyName("suspects")]
    internal PedConfig[] Suspects { get; set; }
}