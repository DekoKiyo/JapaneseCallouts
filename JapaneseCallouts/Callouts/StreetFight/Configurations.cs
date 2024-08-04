namespace JapaneseCallouts.Callouts.StreetFight;

internal class Configurations : IConfig
{
    [JsonPropertyName("suspects")]
    internal PedConfig[] Suspects { get; set; }
}