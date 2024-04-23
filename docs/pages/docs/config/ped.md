# Ped settings

::: info
- If you enable \"random_props\" option, the config which start with\"comp\_\" and \"prop\_\" will be disabled.
- A probability calculation is performed after extracting the presets that appear to the weather at the time of appearance from all the presets.
- If there are no presets set to spawn in the weather at the time of appearance, all presets will be used for probability calculations.
:::

|   Setting Name   |                Description                |  Init Value  |
| :--------------: | :---------------------------------------: | :----------: |
|    **chance**    |      The rate of using this preset.       |     100      |
| **random_props** |      Set the model and props random.      |    false     |
|    **health**    |            Ped's health value             |     200      |
|    **armor**     |             Ped's armor value             |     200      |
|   **is_sunny**   |    Using this preset in sunny weather     |     true     |
|   **is_rainy**   |     Using this preset in sunny rainy      |    false     |
|   **is_snowy**   |     Using this preset in sunny snowy      |    false     |
|  **Ped Model**   | Enter the ped model in the element of XML | Empty String |

::: info
If you enable \"random_props\" option, the config which start with\"comp\_\" and \"prop\_\" will be disabled.
And the value is \"0\" or omitted, it will be disabled.<br/>
If you want to use EUP outfits, you can use \"EUPConverter\" to generate the code easily.<br/>
All init values are \"0\" and everything can omit.
:::

|         Setting Name         |        Description         |
| :--------------------------: | :------------------------: |
|     **comp_mask_model**      |     The model of Mask      |
|   **comp_upperskin_model**   |   The model of Upperskin   |
|     **comp_pants_model**     |     The model of Pants     |
|   **comp_parachute_model**   |   The model of Parachute   |
|     **comp_shoes_model**     |     The model of Shoes     |
|  **comp_accessories_model**  |  The model of Accessories  |
|   **comp_undercoat_model**   |   The model of Undercoat   |
|     **comp_armor_model**     |     The model of Armor     |
|     **comp_decal_model**     |     The model of Decal     |
|      **comp_top_model**      |      The model of Top      |
|    **comp_mask_texture**     |    The texture of Mask     |
|  **comp_upperskin_texture**  |  The texture of Upperskin  |
|    **comp_pants_texture**    |    The texture of Pants    |
|  **comp_parachute_texture**  |  The texture of Parachute  |
|    **comp_shoes_texture**    |    The texture of Shoes    |
| **comp_accessories_texture** | The texture of Accessories |
|  **comp_undercoat_texture**  |  The texture of Undercoat  |
|    **comp_armor_texture**    |    The texture of Armor    |
|    **comp_decal_texture**    |    The texture of Decal    |
|     **comp_top_texture**     |     The texture of Top     |
|      **prop_hat_model**      |      The model of Hat      |
|    **prop_glasses_model**    |    The model of Glasses    |
|      **prop_ear_model**      |      The model of Ear      |
|     **prop_watch_model**     |     The model of Watch     |
|     **prop_hat_texture**     |     The texture of Hat     |
|   **prop_glasses_texture**   |   The texture of Glasses   |
|     **prop_ear_texture**     |     The texture of Ear     |
|    **prop_watch_texture**    |    The texture of Watch    |

### XML Example
Example 1
```xml:line-numbers
<PoliceOfficerModels>
  <!-- Male -->
  <!-- LSPD Class A -->
  <Ped chance="30" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="201" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="1" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
  <!-- LSPD Jacket -->
  <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="true" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="30" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="29" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
  <!-- LSPD Raincoat -->
  <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="188" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="29" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>

  <!-- Female -->
  <!-- LSPD Class A -->
  <Ped chance="30" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="203" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="1" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
  <!-- LSPD Jacket -->
  <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="true" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="201" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="31" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
  <!-- LSPD Raincoat -->
  <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="190" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="31" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>

  <Ped chance="20" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" random_props="true">S_M_Y_COP_01</Ped>
  <Ped chance="20" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" random_props="true">S_F_Y_COP_01</Ped>
</PoliceOfficerModels>
```

Example 2
```xml:line-numbers
<HostageModels>
  <Ped chance="20" random_props="true">A_M_M_BUSINESS_01</Ped>
  <Ped chance="20" random_props="true">A_M_Y_BUSINESS_01</Ped>
  <Ped chance="20" random_props="true">A_M_Y_BUSINESS_02</Ped>
</HostageModels>
```