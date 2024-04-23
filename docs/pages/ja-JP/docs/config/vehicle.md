# 車両 (Vehicle) の設定

::: info
- 車両にlivery(テクスチャ)が存在しない場合は、「livery」は無効になり、後述の車両色の設定が適用されます。
- 車両色はRGBで指定します。詳しくは[こちら](https://www.w3schools.com/colors/colors_rgb.asp)を参照してください。
- RGBの3つの値全てが0以上255でないと正常に色を指定することができません。
- 車両にlivery(テクスチャ)が存在するときは、この設定は無効になります。
- liveryと車両色が共に未設定の場合は、初期状態の色またはliveryでスポーンします。
:::

|      設定名       |                         説明                         |   初期値   |
| :---------------: | :--------------------------------------------------: | :--------: |
|    **chance**     |            このプリセットが使用される確率            |    100     |
|    **livery**     |                車両のテクスチャを指定                |     -1     |
|    **color_r**    |                 RGBのRを指定 [0-255]                 |     -1     |
|    **color_g**    |                 RGBのGを指定 [0-255]                 |     -1     |
|    **color_b**    |                 RGBのBを指定 [0-255]                 |     -1     |
| **Vehicleモデル** | XmlのエレメントにVehicleのモデルを入力してください。 | 空の文字列 |

### XML例
例1
```xml:line-numbers
<Vehicle chance="50">POLICE</Vehicle>
```

例2
```xml:line-numbers
<Vehicle chance="100" livery="2">POLICE</Vehicle>
```

例3
```xml:line-numbers
<Vehicle chance="10" color_r="10" color_g="250" color_b="110">POLICE</Vehicle>
```