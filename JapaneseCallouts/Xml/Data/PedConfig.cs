namespace JapaneseCallouts.Xml.Data;

public class PedConfig : IChanceObject, IEntityObject
{
    [XmlAttribute("chance")]
    public int Chance { get; set; } = 100;
    [XmlAttribute("random_props")]
    public bool RandomProps { get; set; } = false;
    [XmlAttribute("health")]
    public int Health { get; set; } = 200;
    [XmlAttribute("armor")]
    public int Armor { get; set; } = 200;
    [XmlAttribute("is_sunny")]
    public bool IsSunny { get; set; } = true;
    [XmlAttribute("is_rainy")]
    public bool IsRainy { get; set; } = false;
    [XmlAttribute("is_snowy")]
    public bool IsSnowy { get; set; } = false;
    [XmlText()]
    public string Model { get; set; } = string.Empty;

    [XmlAttribute("comp_mask_model")]
    public int MaskModel { get; set; } = 0;
    [XmlAttribute("comp_upperskin_model")]
    public int UpperSkinModel { get; set; } = 0;
    [XmlAttribute("comp_pants_model")]
    public int PantsModel { get; set; } = 0;
    [XmlAttribute("comp_parachute_model")]
    public int ParachuteModel { get; set; } = 0;
    [XmlAttribute("comp_shoes_model")]
    public int ShoesModel { get; set; } = 0;
    [XmlAttribute("comp_accessories_model")]
    public int AccessoriesModel { get; set; } = 0;
    [XmlAttribute("comp_undercoat_model")]
    public int UndercoatModel { get; set; } = 0;
    [XmlAttribute("comp_armor_model")]
    public int ArmorModel { get; set; } = 0;
    [XmlAttribute("comp_decal_model")]
    public int DecalModel { get; set; } = 0;
    [XmlAttribute("comp_top_model")]
    public int TopModel { get; set; } = 0;
    [XmlAttribute("comp_mask_texture")]
    public int MaskTexture { get; set; } = 0;
    [XmlAttribute("comp_upperskin_texture")]
    public int UpperSkinTexture { get; set; } = 0;
    [XmlAttribute("comp_pants_texture")]
    public int PantsTexture { get; set; } = 0;
    [XmlAttribute("comp_parachute_texture")]
    public int ParachuteTexture { get; set; } = 0;
    [XmlAttribute("comp_shoes_texture")]
    public int ShoesTexture { get; set; } = 0;
    [XmlAttribute("comp_accessories_texture")]
    public int AccessoriesTexture { get; set; } = 0;
    [XmlAttribute("comp_undercoat_texture")]
    public int UndercoatTexture { get; set; } = 0;
    [XmlAttribute("comp_armor_texture")]
    public int ArmorTexture { get; set; } = 0;
    [XmlAttribute("comp_decal_texture")]
    public int DecalTexture { get; set; } = 0;
    [XmlAttribute("comp_top_texture")]
    public int TopTexture { get; set; } = 0;

    [XmlAttribute("prop_hat_model")]
    public int HatModel { get; set; } = 0;
    [XmlAttribute("prop_glasses_model")]
    public int GlassesModel { get; set; } = 0;
    [XmlAttribute("prop_ear_model")]
    public int EarModel { get; set; } = 0;
    [XmlAttribute("prop_watch_model")]
    public int WatchModel { get; set; } = 0;
    [XmlAttribute("prop_hat_texture")]
    public int HatTexture { get; set; } = 0;
    [XmlAttribute("prop_glasses_texture")]
    public int GlassesTexture { get; set; } = 0;
    [XmlAttribute("prop_ear_texture")]
    public int EarTexture { get; set; } = 0;
    [XmlAttribute("prop_watch_texture")]
    public int WatchTexture { get; set; } = 0;
}