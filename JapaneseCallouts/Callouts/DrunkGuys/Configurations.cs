namespace JapaneseCallouts.Callouts.DrunkGuys;

internal class Configurations
{
    [JsonPropertyName("callout_positions")]
    internal DrunkGuysPosition[] DrunkGuysPositions { get; set; }
}

internal class DrunkGuysPosition
{
    [JsonPropertyName("x")]
    internal float X { get; set; }
    [JsonPropertyName("y")]
    internal float Y { get; set; }
    [JsonPropertyName("z")]
    internal float Z { get; set; }
    [JsonPropertyName("heading")]
    internal float Heading { get; set; }
    [JsonPropertyName("drunk_positions")]
    internal Position[] DrunkPos { get; set; }
}