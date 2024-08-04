namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal class Configurations
{
    // Numbers
    [JsonProperty("hostage_count")]
    public int HostageCount { get; set; }
    [JsonProperty("wife_name")]
    public string WifeName { get; set; }

    // Entities
    [JsonProperty("police_cruisers")]
    public VehicleConfig[] PoliceCruisers { get; set; }
    [JsonProperty("police_transporters")]
    public VehicleConfig[] PoliceTransporters { get; set; }
    [JsonProperty("police_riots")]
    public VehicleConfig[] PoliceRiots { get; set; }
    [JsonProperty("ambulances")]
    public VehicleConfig[] Ambulances { get; set; }
    [JsonProperty("firetrucks")]
    public VehicleConfig[] Firetrucks { get; set; }
    [JsonProperty("police_officers_models")]
    public PedConfig[] PoliceOfficerModels { get; set; }
    [JsonProperty("police_swat_models")]
    public PedConfig[] PoliceSWATModels { get; set; }
    [JsonProperty("paramedic_models")]
    public PedConfig[] ParamedicModels { get; set; }
    [JsonProperty("firefighter_models")]
    public PedConfig[] FirefighterModels { get; set; }
    [JsonProperty("commander_models")]
    public PedConfig[] CommanderModels { get; set; }
    [JsonProperty("wife_models")]
    public PedConfig[] WifeModels { get; set; }
    [JsonProperty("robber_models")]
    public PedConfig[] RobberModels { get; set; }
    [JsonProperty("hostage_models")]
    public PedConfig[] HostageModels { get; set; }
    [JsonProperty("officer_weapons")]
    public WeaponConfig[] OfficerWeapons { get; set; }
    [JsonProperty("swat_weapons")]
    public WeaponConfig[] SWATWeapons { get; set; }
    [JsonProperty("robbers_weapons")]
    public WeaponConfig[] RobbersWeapons { get; set; }
    [JsonProperty("robbers_sub_weapons")]
    public WeaponConfig[] RobbersThrowableWeapons { get; set; }
    [JsonProperty("weapon_in_riot")]
    public WeaponConfig[] WeaponInRiot { get; set; }

    // Positions
    [JsonProperty("police_cruiser_positions")]
    public Position[] PoliceCruiserPositions { get; set; }
    [JsonProperty("police_transporter_positions")]
    public Position[] PoliceTransportPositions { get; set; }
    [JsonProperty("riot_positions")]
    public Position[] RiotPositions { get; set; }
    [JsonProperty("ambulance_positions")]
    public Position[] AmbulancePositions { get; set; }
    [JsonProperty("firetruck_positions")]
    public Position[] FiretruckPositions { get; set; }
    [JsonProperty("barrier_positions")]
    public Position[] BarrierPositions { get; set; }
    [JsonProperty("aiming_officer_positions")]
    public Position[] AimingOfficerPositions { get; set; }
    [JsonProperty("standing_officer_positions")]
    public Position[] StandingOfficerPositions { get; set; }
    [JsonProperty("normal_robbers_positions")]
    public Position[] NormalRobbersPositions { get; set; }
    [JsonProperty("robbers_negotiation_positions")]
    public Position[] RobbersNegotiationPositions { get; set; }
    [JsonProperty("robbers_sneak_position")]
    public RobbersSneakPosition[] RobbersSneakPosition { get; set; }
    [JsonProperty("robbers_in_vault_positions")]
    public Position[] RobbersInVaultPositions { get; set; }
    [JsonProperty("robbers_surrendering_positions")]
    public Position[] RobbersSurrenderingPositions { get; set; }
    [JsonProperty("firefighter_positions")]
    public Position[] FirefighterPositions { get; set; }
    [JsonProperty("paramedic_positions")]
    public Position[] ParamedicPositions { get; set; }
    [JsonProperty("left_sitting_swat_positions")]
    public Position[] LeftSittingSWATPositions { get; set; }
    [JsonProperty("right_sitting_swat_positions")]
    public Position[] RightSittingSWATPositions { get; set; }
    [JsonProperty("right_looking_swat_positions")]
    public Position[] RightLookingSWATPositions { get; set; }
    [JsonProperty("hostage_positions")]
    public PositionBase[] HostagePositions { get; set; }
    [JsonProperty("hostage_safe_position")]
    public Position HostageSafePosition { get; set; }
    [JsonProperty("commander_position")]
    public Position CommanderPosition { get; set; }
    [JsonProperty("wife_position")]
    public Position WifePosition { get; set; }
    [JsonProperty("wife_vehicle_destination")]
    public PositionBase WifeVehicleDestination { get; set; }
}

internal class RobbersSneakPosition : Position
{
    [JsonProperty("is_right")]
    internal bool IsRight { get; set; }
}