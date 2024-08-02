namespace JapaneseCallouts.Configurations;

internal class VehicleConfig : IChanceObject, IModelObject
{
    [JsonProperty]
    public int Chance { get; set; } = 100;
    [JsonProperty]
    internal int Livery { get; set; } = -1;
    [JsonProperty]
    internal int ColorR { get; set; } = -1;
    [JsonProperty]
    internal int ColorG { get; set; } = -1;
    [JsonProperty]
    internal int ColorB { get; set; } = -1;
    [JsonProperty, JsonRequired]
    public string Model { get; set; }
}