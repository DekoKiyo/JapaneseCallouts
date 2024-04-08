namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("BankHeistConfig")]
public class BankHeistConfig
{
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
}