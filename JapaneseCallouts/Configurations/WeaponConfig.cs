namespace JapaneseCallouts.Configurations;

internal class WeaponConfig : IChanceObject, IEntityObject
{
    [JsonProperty]
    public int Chance { get; set; } = 100;
    [JsonProperty, JsonRequired]
    public string Model { get; set; }
    [JsonProperty]
    internal string[] Components { get; set; } = [];
}