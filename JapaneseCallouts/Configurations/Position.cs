namespace JapaneseCallouts.Configurations;

internal class PositionBase
{
    [JsonProperty, JsonRequired]
    internal float X { get; set; }
    [JsonProperty, JsonRequired]
    internal float Y { get; set; }
    [JsonProperty, JsonRequired]
    internal float Z { get; set; }
}

internal class Position : PositionBase
{
    [JsonProperty, JsonRequired]
    internal float Heading { get; set; }
}