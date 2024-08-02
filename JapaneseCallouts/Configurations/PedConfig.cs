namespace JapaneseCallouts.Configurations;

internal class PedConfig : IChanceObject, IEntityObject
{
    [JsonProperty]
    public int Chance { get; set; } = 100;
    [JsonProperty]
    internal bool RandomProps { get; set; } = false;
    [JsonProperty]
    internal int Health { get; set; } = 200;
    [JsonProperty]
    internal int Armor { get; set; } = 200;
    [JsonProperty]
    internal bool IsSunny { get; set; } = true;
    [JsonProperty]
    internal bool IsRainy { get; set; } = false;
    [JsonProperty]
    internal bool IsSnowy { get; set; } = false;
    [JsonProperty, JsonRequired]
    public string Model { get; set; }

    [JsonProperty]
    internal int MaskModel { get; set; } = 0;
    [JsonProperty]
    internal int UpperSkinModel { get; set; } = 0;
    [JsonProperty]
    internal int PantsModel { get; set; } = 0;
    [JsonProperty]
    internal int ParachuteModel { get; set; } = 0;
    [JsonProperty]
    internal int ShoesModel { get; set; } = 0;
    [JsonProperty]
    internal int AccessoriesModel { get; set; } = 0;
    [JsonProperty]
    internal int UndercoatModel { get; set; } = 0;
    [JsonProperty]
    internal int ArmorModel { get; set; } = 0;
    [JsonProperty]
    internal int DecalModel { get; set; } = 0;
    [JsonProperty]
    internal int TopModel { get; set; } = 0;
    [JsonProperty]
    internal int MaskTexture { get; set; } = 0;
    [JsonProperty]
    internal int UpperSkinTexture { get; set; } = 0;
    [JsonProperty]
    internal int PantsTexture { get; set; } = 0;
    [JsonProperty]
    internal int ParachuteTexture { get; set; } = 0;
    [JsonProperty]
    internal int ShoesTexture { get; set; } = 0;
    [JsonProperty]
    internal int AccessoriesTexture { get; set; } = 0;
    [JsonProperty]
    internal int UndercoatTexture { get; set; } = 0;
    [JsonProperty]
    internal int ArmorTexture { get; set; } = 0;
    [JsonProperty]
    internal int DecalTexture { get; set; } = 0;
    [JsonProperty]
    internal int TopTexture { get; set; } = 0;

    [JsonProperty]
    internal int HatModel { get; set; } = 0;
    [JsonProperty]
    internal int GlassesModel { get; set; } = 0;
    [JsonProperty]
    internal int EarModel { get; set; } = 0;
    [JsonProperty]
    internal int WatchModel { get; set; } = 0;
    [JsonProperty]
    internal int HatTexture { get; set; } = 0;
    [JsonProperty]
    internal int GlassesTexture { get; set; } = 0;
    [JsonProperty]
    internal int EarTexture { get; set; } = 0;
    [JsonProperty]
    internal int WatchTexture { get; set; } = 0;
}