namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("BankHeistConfig")]
public class BankHeistConfig
{
    [XmlArray("Banks"), XmlArrayItem("Bank")]
    public List<BankData> BankData { get; set; }
    [XmlArray("Robbers"), XmlArrayItem("Ped")]
    public List<PedConfig> Robbers { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> Weapons { get; set; }
}

public class BankData
{
    [XmlAttribute("x")]
    public float X { get; set; }
    [XmlAttribute("y")]
    public float Y { get; set; }
    [XmlAttribute("z")]
    public float Z { get; set; }
    [XmlArray("Robbers"), XmlArrayItem("Position")]
    public List<Position> RobbersPos { get; set; }
}