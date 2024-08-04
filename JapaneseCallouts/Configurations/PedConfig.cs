namespace JapaneseCallouts.Configurations;

internal class PedConfig : IChanceObject
{
    [JsonProperty("chance")]
    public int Chance { get; set; } = 100;
    [JsonProperty("random_props")]
    internal bool RandomProps { get; set; } = false;
    [JsonProperty("health")]
    internal int Health { get; set; } = 200;
    [JsonProperty("armor")]
    internal int Armor { get; set; } = 200;
    [JsonProperty("is_sunny")]
    internal bool IsSunny { get; set; } = true;
    [JsonProperty("is_rainy")]
    internal bool IsRainy { get; set; } = false;
    [JsonProperty("is_snowy")]
    internal bool IsSnowy { get; set; } = false;
    [JsonProperty("outfit_name"), JsonRequired]
    internal string OutfitName { get; set; }
}