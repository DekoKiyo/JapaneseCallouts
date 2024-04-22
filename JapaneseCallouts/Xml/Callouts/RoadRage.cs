namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("RoadRageConfig")]
public class RoadRageConfig
{
    [XmlArray("Vehicles"), XmlArrayItem("Vehicle")]
    public List<VehicleConfig> Vehicles { get; set; }
    [XmlArray("Peds"), XmlArrayItem("Ped")]
    public List<PedConfig> Peds { get; set; }
}