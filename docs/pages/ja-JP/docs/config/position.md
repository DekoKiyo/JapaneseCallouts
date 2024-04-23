# Position (位置) の設定

::: warning
- この設定は全て初期値を指定していないため、値を入力しなければエラーが発生します。
- 一部の設定ではheadingが指定できません。<br/>
 詳しくは各設定の説明を確認してください。
:::

|設定名|説明|
|:-:|:-:|
|**x**|X座標|
|**y**|Y座標|
|**z**|Z座標|
|**heading**|向き|

### XML例
例1
```xml:line-numbers
<PoliceCruiserPositions>
  <Position x="271.3" y="180.6" z="104.3" heading="69.4" />
  <Position x="258.9" y="185.2" z="104.4" heading="69.8" />
  <Position x="246.8" y="189.8" z="104.8" heading="68.1" />
</PoliceCruiserPositions>
```

例2
```xml:line-numbers
<HostagePositions>
  <Position x="242.8" y="228.3" z="106.2" />
  <Position x="248.3" y="229.7" z="106.2" />
  <Position x="241.4" y="221.5" z="106.2" />
</HostagePositions>
```