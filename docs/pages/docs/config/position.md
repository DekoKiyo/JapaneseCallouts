# Position settings

::: warning
- Because this setting has no init value, you have to enter some values to avoid the crash.
- Some settings can't set the \"heading\" setting.<br/>
 Check more information in each callouts description.
:::

| Setting Name | Description |
| :----------: | :---------: |
|    **x**     |   X coord   |
|    **y**     |   Y coord   |
|    **z**     |   Z coord   |
| **heading**  |   heading   |

### XML Example
Example 1
```xml:line-numbers
<PoliceCruiserPositions>
  <Position x="271.3" y="180.6" z="104.3" heading="69.4" />
  <Position x="258.9" y="185.2" z="104.4" heading="69.8" />
  <Position x="246.8" y="189.8" z="104.8" heading="68.1" />
</PoliceCruiserPositions>
```

Example 2
```xml:line-numbers
<HostagePositions>
  <Position x="242.8" y="228.3" z="106.2" />
  <Position x="248.3" y="229.7" z="106.2" />
  <Position x="241.4" y="221.5" z="106.2" />
</HostagePositions>
```