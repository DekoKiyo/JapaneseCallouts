namespace JapaneseCallouts.Configurations;

internal class PositionBase
{
    [JsonPropertyName("x")]
    internal float X { get; set; }
    [JsonPropertyName("y")]
    internal float Y { get; set; }
    [JsonPropertyName("z")]
    internal float Z { get; set; }
}

internal class Position : PositionBase
{
    [JsonPropertyName("heading")]
    internal float Heading { get; set; }
}