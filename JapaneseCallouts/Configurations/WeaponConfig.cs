namespace JapaneseCallouts.Configurations;

internal class WeaponConfig : IChanceObject, IModelObject
{
    [JsonPropertyName("chance")]
    public int Chance { get; set; } = 100;
    [JsonPropertyName("model")]
    public string Model { get; set; }
    [JsonPropertyName("components")]
    internal string[] Components { get; set; } = [];
}