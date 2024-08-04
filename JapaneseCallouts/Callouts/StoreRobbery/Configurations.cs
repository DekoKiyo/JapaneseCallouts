namespace JapaneseCallouts.Callouts.StoreRobbery;

internal class Configurations
{
    [JsonPropertyName("robber_peds")]
    internal PedConfig[] RobberPeds { get; set; }
    [JsonPropertyName("weapons")]
    internal WeaponConfig[] Weapons { get; set; }
    [JsonPropertyName("stores")]
    internal StoreRobberyPosition[] Stores { get; set; }
}

internal class StoreRobberyPosition
{
    [JsonPropertyName("x")]
    internal float X { get; set; }
    [JsonPropertyName("y")]
    internal float Y { get; set; }
    [JsonPropertyName("z")]
    internal float Z { get; set; }
    [JsonPropertyName("robbers_positions")]
    internal PositionBase[] RobbersPositions { get; set; }
}