namespace JapaneseCallouts.Configurations;

internal class VehicleConfig : IChanceObject, IModelObject
{
    [JsonPropertyName("chance")]
    public int Chance { get; set; } = 100;
    [JsonPropertyName("livery")]
    internal int Livery { get; set; } = -1;
    [JsonPropertyName("color_r")]
    internal int ColorR { get; set; } = -1;
    [JsonPropertyName("color_g")]
    internal int ColorG { get; set; } = -1;
    [JsonPropertyName("color_b")]
    internal int ColorB { get; set; } = -1;
    [JsonPropertyName("model")]
    public string Model { get; set; }
}