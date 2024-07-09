namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("StoreRobberyConfig")]
public class StoreRobberyConfig
{
    [XmlArray("RobberPeds"), XmlArrayItem("Ped")]
    public List<PedConfig> RobberPeds { get; set; }
    [XmlArray("Weapons"), XmlArrayItem("Weapon")]
    public List<WeaponConfig> Weapons { get; set; }
    [XmlArray("Stores"), XmlArrayItem("Store")]
    public List<StoreRobberyPosition> Stores { get; set; }
}

public class StoreRobberyPosition
{
    [XmlAttribute("x")]
    public float Store_X { get; set; }
    [XmlAttribute("y")]
    public float Store_Y { get; set; }
    [XmlAttribute("z")]
    public float Store_Z { get; set; }
    [XmlArray("RobbersPositions"), XmlArrayItem("Position")]
    public List<PositionBase> RobbersPositions { get; set; }
}