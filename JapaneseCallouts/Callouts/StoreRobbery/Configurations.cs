namespace JapaneseCallouts.Callouts.StoreRobbery;

internal class Configurations
{
    [JsonProperty("robber_peds")]
    internal PedConfig[] RobberPeds { get; set; }
    [JsonProperty("weapons")]
    internal WeaponConfig[] Weapons { get; set; }
    [JsonProperty("stores")]
    internal StoreRobberyPosition[] Stores { get; set; }
}

internal class StoreRobberyPosition
{
    [JsonProperty("x")]
    internal float X { get; set; }
    [JsonProperty("y")]
    internal float Y { get; set; }
    [JsonProperty("z")]
    internal float Z { get; set; }
    [JsonProperty("robbers_positions")]
    internal PositionBase[] RobbersPositions { get; set; }
}