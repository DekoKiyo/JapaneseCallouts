# Vehicle settings

::: info

- If the vehicle has no livery, the \"livery\" setting will be disabled and enabled the color settings.
- You can use the RGB to set the vehicle color. More information is [here](https://www.w3schools.com/colors/colors_rgb.asp).
- It will be enabled when all of the three (R, G, and B) values are 0 to 255.
- If vehicle has some livery, color settings will be disabled.
- \"livery\" and all color settings are omitted, the vehicle will be spawned with initialized.
  :::

|           Setting Name           |                               Description                               |  Init value  |
| :------------------------------: | :---------------------------------------------------------------------: | :----------: |
|            **chance**            |              The rate of using this preset.             |      100     |
|            **livery**            |                        Set the livery of vehicle                        |      -1      |
| **color_r** | Set RGB's R [0-255] |      -1      |
| **color_g** | Set RGB's G [0-255] |      -1      |
| **color_b** | Set RGB's B [0-255] |      -1      |
|         **Vehicle Model**        |              Enter the vehicle model in the element of XML              | Empty String |

### XML Example

Example 1

```xml:line-numbers
<Vehicle chance="50">POLICE</Vehicle>
```

Example 2

```xml:line-numbers
<Vehicle chance="100" livery="2">POLICE</Vehicle>
```

Example 3

```xml:line-numbers
<Vehicle chance="10" color_r="10" color_g="250" color_b="110">POLICE</Vehicle>
```
