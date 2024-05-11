namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("HotPursuitConfig")]
public class HotPursuitConfig
{
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> Vehicles { get; set; }
}