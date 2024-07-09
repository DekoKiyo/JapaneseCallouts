namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("RoadRageConfig")]
public class RoadRageConfig
{
    [XmlArray("VictimVehicles"), XmlArrayItem("Vehicle")]
    public List<VehicleConfig> VictimVehicles { get; set; }
    [XmlArray("SuspectVehicles"), XmlArrayItem("Vehicle")]
    public List<VehicleConfig> SuspectVehicles { get; set; }
    [XmlArray("VictimPeds"), XmlArrayItem("Ped")]
    public List<PedConfig> VictimPeds { get; set; }
    [XmlArray("SuspectPeds"), XmlArrayItem("Ped")]
    public List<PedConfig> SuspectPeds { get; set; }
}