namespace JapaneseCallouts.Callouts.WantedCriminalFound;

internal class Configurations
{
    [JsonProperty("criminals")]
    internal PedConfig[] Criminals { get; set; }
    [JsonProperty("weapons")]
    internal WeaponConfig[] Weapons { get; set; }
}