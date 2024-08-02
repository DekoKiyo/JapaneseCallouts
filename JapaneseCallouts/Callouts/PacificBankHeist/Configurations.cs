namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal class Configurations
{
    // Numbers
    [JsonProperty]
    public int HostageCount { get; set; }
    [JsonProperty]
    public string WifeName { get; set; }

    // Entities
    [JsonProperty]
    public VehicleConfig[] PoliceCruisers { get; set; }
    [JsonProperty]
    public VehicleConfig[] PoliceTransporters { get; set; }
    [JsonProperty]
    public VehicleConfig[] PoliceRiots { get; set; }
    [JsonProperty]
    public VehicleConfig[] Ambulances { get; set; }
    [JsonProperty]
    public VehicleConfig[] Firetrucks { get; set; }
    [JsonProperty]
    public PedConfig[] PoliceOfficerModels { get; set; }
    [JsonProperty]
    public PedConfig[] PoliceSWATModels { get; set; }
    [JsonProperty]
    public PedConfig[] ParamedicModels { get; set; }
    [JsonProperty]
    public PedConfig[] FirefighterModels { get; set; }
    [JsonProperty]
    public PedConfig[] CommanderModels { get; set; }
    [JsonProperty]
    public PedConfig[] WifeModels { get; set; }
    [JsonProperty]
    public PedConfig[] RobberModels { get; set; }
    [JsonProperty]
    public PedConfig[] HostageModels { get; set; }
    [JsonProperty]
    public WeaponConfig[] OfficerWeapons { get; set; }
    [JsonProperty]
    public WeaponConfig[] SWATWeapons { get; set; }
    [JsonProperty]
    public WeaponConfig[] RobbersWeapons { get; set; }
    [JsonProperty]
    public WeaponConfig[] RobbersThrowableWeapons { get; set; }
    [JsonProperty]
    public WeaponConfig[] WeaponInRiot { get; set; }

    // Positions
    [JsonProperty]
    public Position[] PoliceCruiserPositions { get; set; }
    [JsonProperty]
    public Position[] PoliceTransportPositions { get; set; }
    [JsonProperty]
    public Position[] RiotPositions { get; set; }
    [JsonProperty]
    public Position[] AmbulancePositions { get; set; }
    [JsonProperty]
    public Position[] FiretruckPositions { get; set; }
    [JsonProperty]
    public Position[] BarrierPositions { get; set; }
    [JsonProperty]
    public Position[] AimingOfficerPositions { get; set; }
    [JsonProperty]
    public Position[] StandingOfficerPositions { get; set; }
    [JsonProperty]
    public Position[] NormalRobbersPositions { get; set; }
    [JsonProperty]
    public Position[] RobbersNegotiationPositions { get; set; }
    [JsonProperty]
    public RobbersSneakPosition[] RobbersSneakPosition { get; set; }
    [JsonProperty]
    public Position[] RobbersInVaultPositions { get; set; }
    [JsonProperty]
    public Position[] RobbersSurrenderingPositions { get; set; }
    [JsonProperty]
    public Position[] FirefighterPositions { get; set; }
    [JsonProperty]
    public Position[] ParamedicPositions { get; set; }
    [JsonProperty]
    public Position[] LeftSittingSWATPositions { get; set; }
    [JsonProperty]
    public Position[] RightSittingSWATPositions { get; set; }
    [JsonProperty]
    public Position[] RightLookingSWATPositions { get; set; }
    [JsonProperty]
    public PositionBase[] HostagePositions { get; set; }
    [XmlElement]
    public Position HostageSafePosition { get; set; }
    [XmlElement]
    public Position CommanderPosition { get; set; }
    [XmlElement]
    public Position WifePosition { get; set; }
    [XmlElement]
    public PositionBase WifeVehicleDestination { get; set; }
}