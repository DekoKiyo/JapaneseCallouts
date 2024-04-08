namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("BankHeistConfig")]
public class BankHeistConfig
{
    // Numbers
    [XmlElement]
    public int HostageCount { get; set; }
    [XmlElement]
    public string WifeName { get; set; }

    // Entities
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> PoliceCruisers { get; set; }
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> PoliceTransporters { get; set; }
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> PoliceRiots { get; set; }
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> Ambulances { get; set; }
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> Firetrucks { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> PoliceOfficerModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> PoliceSWATModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> ParamedicModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> FirefighterModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> CommanderModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> WifeModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> RobberModels { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> HostageModels { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> OfficerWeapons { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> SWATWeapons { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> RobbersWeapons { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> RobbersThrowableWeapons { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> WeaponInRiot { get; set; }

    // Positions
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> PoliceCruiserPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> PoliceTransportPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> RiotPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> AmbulancePositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> FiretruckPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> BarrierPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> AimingOfficerPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> StandingOfficerPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> NormalRobbersPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> RobbersNegotiationPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<RobbersSneakPosition> RobbersSneakPosition { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> RobbersInVaultPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> RobbersSurrenderingPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> FirefighterPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> ParamedicPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> LeftSittingSWATPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> RightSittingSWATPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> RightLookingSWATPositions { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<PositionBase> HostagePositions { get; set; }
    [XmlElement]
    public Position HostageSafePosition { get; set; }
    [XmlElement]
    public Position CommanderPosition { get; set; }
    [XmlElement]
    public Position WifePosition { get; set; }
    [XmlElement]
    public PositionBase WifeVehicleDestination { get; set; }
}

public class PositionBase
{
    [XmlAttribute("x")]
    public float X { get; set; }
    [XmlAttribute("y")]
    public float Y { get; set; }
    [XmlAttribute("z")]
    public float Z { get; set; }
}

public class Position : PositionBase
{
    [XmlAttribute("heading")]
    public float Heading { get; set; }
}

public class RobbersSneakPosition : Position
{
    [XmlAttribute("is_right")]
    public bool IsRight { get; set; }
}