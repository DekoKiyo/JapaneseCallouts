namespace JapaneseCallouts.Configurations;

internal class OutfitConfig
{
    [JsonPropertyName("model"), JsonRequired]
    internal string Model { get; set; }
    [JsonPropertyName("comp_mask_model")]
    internal int MaskModel { get; set; } = 0;
    [JsonPropertyName("comp_upperskin_model")]
    internal int UpperSkinModel { get; set; } = 0;
    [JsonPropertyName("comp_pants_model")]
    internal int PantsModel { get; set; } = 0;
    [JsonPropertyName("comp_parachute_model")]
    internal int ParachuteModel { get; set; } = 0;
    [JsonPropertyName("comp_shoes_model")]
    internal int ShoesModel { get; set; } = 0;
    [JsonPropertyName("comp_accessories_model")]
    internal int AccessoriesModel { get; set; } = 0;
    [JsonPropertyName("comp_undercoat_model")]
    internal int UndercoatModel { get; set; } = 0;
    [JsonPropertyName("comp_armor_model")]
    internal int ArmorModel { get; set; } = 0;
    [JsonPropertyName("comp_decal_model")]
    internal int DecalModel { get; set; } = 0;
    [JsonPropertyName("comp_top_model")]
    internal int TopModel { get; set; } = 0;
    [JsonPropertyName("comp_mask_texture")]
    internal int MaskTexture { get; set; } = 0;
    [JsonPropertyName("comp_upperskin_texture")]
    internal int UpperSkinTexture { get; set; } = 0;
    [JsonPropertyName("comp_pants_texture")]
    internal int PantsTexture { get; set; } = 0;
    [JsonPropertyName("comp_parachute_texture")]
    internal int ParachuteTexture { get; set; } = 0;
    [JsonPropertyName("comp_shoes_texture")]
    internal int ShoesTexture { get; set; } = 0;
    [JsonPropertyName("comp_accessories_texture")]
    internal int AccessoriesTexture { get; set; } = 0;
    [JsonPropertyName("comp_undercoat_texture")]
    internal int UndercoatTexture { get; set; } = 0;
    [JsonPropertyName("comp_armor_texture")]
    internal int ArmorTexture { get; set; } = 0;
    [JsonPropertyName("comp_decal_texture")]
    internal int DecalTexture { get; set; } = 0;
    [JsonPropertyName("comp_top_texture")]
    internal int TopTexture { get; set; } = 0;
    [JsonPropertyName("prop_hat_model")]
    internal int HatModel { get; set; } = 0;
    [JsonPropertyName("prop_glasses_model")]
    internal int GlassesModel { get; set; } = 0;
    [JsonPropertyName("prop_ear_model")]
    internal int EarModel { get; set; } = 0;
    [JsonPropertyName("prop_watch_model")]
    internal int WatchModel { get; set; } = 0;
    [JsonPropertyName("prop_hat_texture")]
    internal int HatTexture { get; set; } = 0;
    [JsonPropertyName("prop_glasses_texture")]
    internal int GlassesTexture { get; set; } = 0;
    [JsonPropertyName("prop_ear_texture")]
    internal int EarTexture { get; set; } = 0;
    [JsonPropertyName("prop_watch_texture")]
    internal int WatchTexture { get; set; } = 0;
}