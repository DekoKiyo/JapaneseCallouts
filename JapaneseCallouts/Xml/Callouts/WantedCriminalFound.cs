namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("WantedCriminalFoundConfig")]
public class WantedCriminalFoundConfig
{
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> Criminals { get; set; }
    [XmlArray, XmlArrayItem("Weapon")]
    public List<WeaponConfig> Weapons { get; set; }
}