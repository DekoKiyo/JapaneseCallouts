namespace JapaneseCallouts.Callouts.DrunkGuys;

internal class Configurations
{
    [JsonProperty("callout_positions")]
    internal DrunkGuysPosition[] DrunkGuysPositions { get; set; }
}

internal class DrunkGuysPosition
{
    [JsonProperty("x")]
    internal float X { get; set; }
    [JsonProperty("y")]
    internal float Y { get; set; }
    [JsonProperty("z")]
    internal float Z { get; set; }
    [JsonProperty("heading")]
    internal float Heading { get; set; }
    [JsonProperty("drunk_positions")]
    internal Position[] DrunkPos { get; set; }
}