namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("EscortConfig")]
public class EscortConfig
{
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> TargetVehicles { get; set; }
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> PoliceVehicles { get; set; }
    [XmlArray, XmlArrayItem("Vehicle")]
    public List<VehicleConfig> AttackerVehicles { get; set; }

    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> TargetPeds { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> PoliceOfficers { get; set; }
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> AttackerPeds { get; set; }

    [XmlArray, XmlArrayItem("Preset")]
    public List<EscortPositionPreset> Presets { get; set; }
}

public class EscortPositionPreset
{
    [XmlElement]
    public Position TargetVehiclePosStart { get; set; }
    [XmlElement]
    public Position EscortPoliceVehicle1PosStart { get; set; }
    [XmlElement]
    public Position EscortPoliceVehicle2PosStart { get; set; }

    [XmlArray, XmlArrayItem("Position")]
    public List<Position> CheckPoints { get; set; }
    [XmlArray, XmlArrayItem("Position")]
    public List<Position> AttackPoints { get; set; }
}