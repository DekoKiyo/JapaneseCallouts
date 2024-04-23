# 武器 (Weapon) の設定

::: info
- 武器については[こちら](https://wiki.rage.mp/index.php?title=Weapons)を参照してください。
- 武器のコンポーネントについては[こちら](https://wiki.rage.mp/index.php?title=Weapons_Components)を参照してください。
:::

|     設定名     |              説明              |   初期値   |   備考   |
| :------------: | :----------------------------: | :--------: | :------: |
|   **chance**   | このプリセットが使用される確率 |    100     |          |
|   **Model**    |         Weaponのモデル         | 空の文字列 |          |
| **Components** |  武器に付けるコンポーネント。  |            | 省略可能 |

### XML例
例1
```xml:line-numbers
<OfficerWeapons>
  <Weapon chance="100">
    <Model>WEAPON_PISTOL</Model>
    <Components>
      <Component>COMPONENT_AT_PI_FLSH</Component>
    </Components>
  </Weapon>
  <Weapon chance="50">
    <Model>WEAPON_COMBATPISTOL</Model>
    <Components>
      <Component>COMPONENT_AT_PI_FLSH</Component>
    </Components>
  </Weapon>
</OfficerWeapons>
```

例2
```xml:line-numbers
<RobbersThrowableWeapons>
  <Weapon chance="50">
    <Model>WEAPON_GRENADE</Model>
  </Weapon>
  <Weapon chance="50">
    <Model>WEAPON_SMOKEGRENADE</Model>
  </Weapon>
</RobbersThrowableWeapons>
```