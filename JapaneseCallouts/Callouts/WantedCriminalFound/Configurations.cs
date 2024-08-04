namespace JapaneseCallouts.Callouts.WantedCriminalFound;

internal class Configurations
{
    [JsonPropertyName("criminals")]
    internal PedConfig[] Criminals { get; set; }
    [JsonPropertyName("weapons")]
    internal WeaponConfig[] Weapons { get; set; }
}