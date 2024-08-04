namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal class Configurations
{
    // Numbers
    [JsonPropertyName("hostage_count")]
    public int HostageCount { get; set; }
    [JsonPropertyName("wife_name")]
    public string WifeName { get; set; }

    // Entities
    [JsonPropertyName("police_cruisers")]
    public VehicleConfig[] PoliceCruisers { get; set; }
    [JsonPropertyName("police_transporters")]
    public VehicleConfig[] PoliceTransporters { get; set; }
    [JsonPropertyName("police_riots")]
    public VehicleConfig[] PoliceRiots { get; set; }
    [JsonPropertyName("ambulances")]
    public VehicleConfig[] Ambulances { get; set; }
    [JsonPropertyName("firetrucks")]
    public VehicleConfig[] Firetrucks { get; set; }
    [JsonPropertyName("police_officers_models")]
    public PedConfig[] PoliceOfficerModels { get; set; }
    [JsonPropertyName("police_swat_models")]
    public PedConfig[] PoliceSWATModels { get; set; }
    [JsonPropertyName("paramedic_models")]
    public PedConfig[] ParamedicModels { get; set; }
    [JsonPropertyName("firefighter_models")]
    public PedConfig[] FirefighterModels { get; set; }
    [JsonPropertyName("commander_models")]
    public PedConfig[] CommanderModels { get; set; }
    [JsonPropertyName("wife_models")]
    public PedConfig[] WifeModels { get; set; }
    [JsonPropertyName("robber_models")]
    public PedConfig[] RobberModels { get; set; }
    [JsonPropertyName("hostage_models")]
    public PedConfig[] HostageModels { get; set; }
    [JsonPropertyName("officer_weapons")]
    public WeaponConfig[] OfficerWeapons { get; set; }
    [JsonPropertyName("swat_weapons")]
    public WeaponConfig[] SWATWeapons { get; set; }
    [JsonPropertyName("robbers_weapons")]
    public WeaponConfig[] RobbersWeapons { get; set; }
    [JsonPropertyName("robbers_sub_weapons")]
    public WeaponConfig[] RobbersThrowableWeapons { get; set; }
    [JsonPropertyName("weapon_in_riot")]
    public WeaponConfig[] WeaponInRiot { get; set; }

    // Positions
    [JsonPropertyName("police_cruiser_positions")]
    public Position[] PoliceCruiserPositions { get; set; }
    [JsonPropertyName("police_transporter_positions")]
    public Position[] PoliceTransportPositions { get; set; }
    [JsonPropertyName("riot_positions")]
    public Position[] RiotPositions { get; set; }
    [JsonPropertyName("ambulance_positions")]
    public Position[] AmbulancePositions { get; set; }
    [JsonPropertyName("firetruck_positions")]
    public Position[] FiretruckPositions { get; set; }
    [JsonPropertyName("barrier_positions")]
    public Position[] BarrierPositions { get; set; }
    [JsonPropertyName("aiming_officer_positions")]
    public Position[] AimingOfficerPositions { get; set; }
    [JsonPropertyName("standing_officer_positions")]
    public Position[] StandingOfficerPositions { get; set; }
    [JsonPropertyName("normal_robbers_positions")]
    public Position[] NormalRobbersPositions { get; set; }
    [JsonPropertyName("robbers_negotiation_positions")]
    public Position[] RobbersNegotiationPositions { get; set; }
    [JsonPropertyName("robbers_sneak_position")]
    public RobbersSneakPosition[] RobbersSneakPosition { get; set; }
    [JsonPropertyName("robbers_in_vault_positions")]
    public Position[] RobbersInVaultPositions { get; set; }
    [JsonPropertyName("robbers_surrendering_positions")]
    public Position[] RobbersSurrenderingPositions { get; set; }
    [JsonPropertyName("firefighter_positions")]
    public Position[] FirefighterPositions { get; set; }
    [JsonPropertyName("paramedic_positions")]
    public Position[] ParamedicPositions { get; set; }
    [JsonPropertyName("left_sitting_swat_positions")]
    public Position[] LeftSittingSWATPositions { get; set; }
    [JsonPropertyName("right_sitting_swat_positions")]
    public Position[] RightSittingSWATPositions { get; set; }
    [JsonPropertyName("right_looking_swat_positions")]
    public Position[] RightLookingSWATPositions { get; set; }
    [JsonPropertyName("hostage_positions")]
    public PositionBase[] HostagePositions { get; set; }
    [JsonPropertyName("hostage_safe_position")]
    public Position HostageSafePosition { get; set; }
    [JsonPropertyName("commander_position")]
    public Position CommanderPosition { get; set; }
    [JsonPropertyName("wife_position")]
    public Position WifePosition { get; set; }
    [JsonPropertyName("wife_vehicle_destination")]
    public PositionBase WifeVehicleDestination { get; set; }
}

internal class RobbersSneakPosition : Position
{
    [JsonPropertyName("is_right")]
    internal bool IsRight { get; set; }
}