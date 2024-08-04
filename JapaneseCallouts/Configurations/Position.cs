namespace JapaneseCallouts.Configurations;

internal class PositionBase
{
    [JsonProperty("x")]
    internal float X { get; set; }
    [JsonProperty("y")]
    internal float Y { get; set; }
    [JsonProperty("z")]
    internal float Z { get; set; }
}

internal class Position : PositionBase
{
    [JsonProperty("heading")]
    internal float Heading { get; set; }
}