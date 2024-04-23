# 人 (Ped) の設定

::: info
- random_propsを有効にすると、「comp\_」と「prop\_」から始まる設定は無効になります。
- 全てのプリセットの中から、出現時の天気で出現するように指定されているプリセットを抽出した後、確率計算が行われます。
- 特定の天気で出現するプリセットが存在しない場合は、全てのプリセットを使用して確立計算が行われます。
:::

|      設定名      |                       説明                       |   初期値   |
| :--------------: | :----------------------------------------------: | :--------: |
|    **chance**    |          このプリセットが使用される確率          |    100     |
| **random_props** |     服装のモデル、テクスチャをランダムにする     |   false    |
|    **health**    |               Pedの体力の値を指定                |    200     |
|    **armor**     |             Pedのアーマーの値を指定              |    200     |
|   **is_sunny**   |           天候が晴れのとき使用されるか           |    true    |
|   **is_rainy**   |            天候が雨のとき使用されるか            |   false    |
|   **is_snowy**   |            天候が雪のとき使用されるか            |   false    |
|  **Pedモデル**   | XmlのエレメントにPedのモデルを入力してください。 | 空の文字列 |

::: info
random_propsが有効の場合は、以下の設定は無効になります。<br/>
また、値を0または省略すると、その設定は無効になります。<br/>
EUPのoutfitを使用したい場合は、「EUPコンバーター」を使用すると、簡単にコードが生成できます。<br/>
初期値は全て0で、全て省略可能です。
:::

|            設定名            |          説明           |
| :--------------------------: | :---------------------: |
|     **comp_mask_model**      |      Maskのモデル       |
|   **comp_upperskin_model**   |    Upperskinのモデル    |
|     **comp_pants_model**     |      Pantsのモデル      |
|   **comp_parachute_model**   |    Parachuteのモデル    |
|     **comp_shoes_model**     |      Shoesのモデル      |
|  **comp_accessories_model**  |   Accessoriesのモデル   |
|   **comp_undercoat_model**   |    Undercoatのモデル    |
|     **comp_armor_model**     |      Armorのモデル      |
|     **comp_decal_model**     |      Decalのモデル      |
|      **comp_top_model**      |       Topのモデル       |
|    **comp_mask_texture**     |    Maskのテクスチャ     |
|  **comp_upperskin_texture**  |  Upperskinのテクスチャ  |
|    **comp_pants_texture**    |    Pantsのテクスチャ    |
|  **comp_parachute_texture**  |  Parachuteのテクスチャ  |
|    **comp_shoes_texture**    |    Shoesのテクスチャ    |
| **comp_accessories_texture** | Accessoriesのテクスチャ |
|  **comp_undercoat_texture**  |  Undercoatのテクスチャ  |
|    **comp_armor_texture**    |    Armorのテクスチャ    |
|    **comp_decal_texture**    |    Decalのテクスチャ    |
|     **comp_top_texture**     |     Topのテクスチャ     |
|      **prop_hat_model**      |       Hatのモデル       |
|    **prop_glasses_model**    |     Glassesのモデル     |
|      **prop_ear_model**      |       Earのモデル       |
|     **prop_watch_model**     |      Watchのモデル      |
|     **prop_hat_texture**     |     Hatのテクスチャ     |
|   **prop_glasses_texture**   |   Glassesのテクスチャ   |
|     **prop_ear_texture**     |     Earのテクスチャ     |
|    **prop_watch_texture**    |    Watchのテクスチャ    |

### XML例
例1
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

例2
```xml:line-numbers
<HostageModels>
  <Ped chance="20" random_props="true">A_M_M_BUSINESS_01</Ped>
  <Ped chance="20" random_props="true">A_M_Y_BUSINESS_01</Ped>
  <Ped chance="20" random_props="true">A_M_Y_BUSINESS_02</Ped>
</HostageModels>
```