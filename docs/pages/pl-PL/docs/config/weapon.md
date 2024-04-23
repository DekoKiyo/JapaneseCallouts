# Weapon settings

::: info

- Check [here](https://wiki.rage.mp/index.php?title=Weapons) about the weapons.
- Check [here](https://wiki.rage.mp/index.php?title=Weapons_Components) about the weapon components.
  :::

|  Setting Name  |                        Description                        |  Init value  |    Note   |
| :------------: | :-------------------------------------------------------: | :----------: | :-------: |
|   **chance**   |       The rate of using this preset.      |      100     |           |
|    **Model**   |                        Weapon Model                       | Empty String |           |
| **Components** | The components that attach to the weapon. |              | Omittable |

### XML Example

Example 1

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

Example 2

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
