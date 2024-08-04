namespace JapaneseCallouts.Configurations;

internal class VehicleConfig : IChanceObject, IModelObject
{
    [JsonProperty("chance")]
    public int Chance { get; set; } = 100;
    [JsonProperty("livery")]
    internal int Livery { get; set; } = -1;
    [JsonProperty("color_r")]
    internal int ColorR { get; set; } = -1;
    [JsonProperty("color_g")]
    internal int ColorG { get; set; } = -1;
    [JsonProperty("color_b")]
    internal int ColorB { get; set; } = -1;
    [JsonProperty("model")]
    public string Model { get; set; }
}