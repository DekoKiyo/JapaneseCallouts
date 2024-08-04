namespace JapaneseCallouts.Configurations;

internal class PedConfig : IChanceObject
{
    [JsonPropertyName("chance")]
    public int Chance { get; set; } = 100;
    [JsonPropertyName("random_props")]
    internal bool RandomProps { get; set; } = false;
    [JsonPropertyName("health")]
    internal int Health { get; set; } = 200;
    [JsonPropertyName("armor")]
    internal int Armor { get; set; } = 200;
    [JsonPropertyName("is_sunny")]
    internal bool IsSunny { get; set; } = true;
    [JsonPropertyName("is_rainy")]
    internal bool IsRainy { get; set; } = false;
    [JsonPropertyName("is_snowy")]
    internal bool IsSnowy { get; set; } = false;
    [JsonPropertyName("outfit_name"), JsonRequired]
    internal string OutfitName { get; set; }
}