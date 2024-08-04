namespace JapaneseCallouts.Configurations;

internal class WeaponConfig : IChanceObject, IModelObject
{
    [JsonProperty("chance")]
    public int Chance { get; set; } = 100;
    [JsonProperty("model")]
    public string Model { get; set; }
    [JsonProperty("components")]
    internal string[] Components { get; set; } = [];
}