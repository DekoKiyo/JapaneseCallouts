
<p align="center">
<img src="./Image/Logo.png">
</p>

<p align="center">
    <a href="https://github.com/DekoKiyo/JapaneseCallouts/releases/latest"><img alt="GitHub Release" src="https://img.shields.io/github/v/release/DekoKiyo/JapaneseCallouts?style=flat&logo=GitHub&label=Latest%20Release"></a>
    <a href="https://github.com/DekoKiyo/JapaneseCallouts/releases/latest"><img alt="GitHub Downloads (all assets, all releases)" src="https://img.shields.io/github/downloads/DekoKiyo/JapaneseCallouts/total?style=flat&logo=GitHub&label=Downloads"></a>
    <a href="https://github.com/DekoKiyo/JapaneseCallouts?tab=GPL-3.0-1-ov-file"><img alt="GitHub License" src="https://img.shields.io/github/license/DekoKiyo/JapaneseCallouts?style=flat&logo=GitHub&label=GPL%20License"></a>
    <a href="https://discord.gg/uTVnVjqQWA"><img alt="Discord" src="https://img.shields.io/discord/1067619328670830682?style=flat&logo=Discord&label=Discord%20Server"></a>
    <a href="https://twitter.com/DekoKiyomori"><img alt="X (formerly Twitter) Follow" src="https://img.shields.io/twitter/follow/DekoKiyomori?style=social&logo=X"></a>
</p>

> [!IMPORTANT]
> **For other mods developers**:<br/>
> This plugin is developed under the [GNU General Public License](https://github.com/DekoKiyo/JapaneseCallouts?tab=GPL-3.0-1-ov-file).<br/>
> Using the code of this project, don't forget to read and respect the license, include the credits, and attach your project GPL v3.

[![Discord](./Image/Discord.png)](https://discord.gg/uTVnVjqQWA)

# Japanese Callouts
The **Japanese Callouts** is a plugin for [LSPDFR](https://discord.gg/uTVnVjqQWA) one of the best police mods of Grand Theft Auto V that adds some Japanese style callouts. This project's source code is under the [GPL v3](https://github.com/DekoKiyo/JapaneseCallouts?tab=GPL-3.0-1-ov-file). You can use or reference its code freedom but you have to write the credits because this project uses many libraries or open-source project code too. _Don't forget to respect them._

## Recommendation

- **EUP Series** to add more emergency services uniforms that use multiplayer characters' more various clothes systems in single-player mode.
  - [EUP L&O](https://www.lcpdfr.com/downloads/gta5mods/character/8151-emergency-uniforms-pack-law-order/)
  - [EUP S&R](https://www.lcpdfr.com/downloads/gta5mods/character/16256-emergency-uniforms-pack-serve-rescue/)
  - [EUP Menu](https://www.lcpdfr.com/downloads/gta5mods/scripts/13245-eup-menu/)

## Releases
| GTA5 Version |   RPH Version    |   LSPDFR Version   | Plugin Version |     Link      |
| :----------: | :--------------: | :----------------: | :------------: | :-----------: |
|   1.0.3095   | 1.98.0 or higher | 0.4.9 (Build 8757) |   Beta.0.1.0   | Not Available |

## Changelog
<details>
    <summary>Click to show the changelog</summary>
</details>

## Installation
### Windows Steam Edition
1. Download the latest [release](https://github.com/DekoKiyo/JapaneseCallouts/releases/latest).
2. Find the GTA5 folder. You can right-click on GTA5 in your library on Steam, a menu will appear, click "Manage", and "Browse local files".
3. Unzip and drag or extract the folders and files from the .zip into your GTA5 folder.
4. Run the GTA5 with Rage Plugin Hook. And check the plugin is successfully activated.

### Windows Epic Edition
1. Download the latest [release](https://github.com/DekoKiyo/JapaneseCallouts/releases/latest).
2. Find the GTA5 folder. Almost all players' GTA5 folders are below.<br/>`C:\Program Files\Epic Games\GTAV`
3. Unzip and drag or extract the folders and files from the .zip into your GTA5 folder.
4. Run the GTA5 with Rage Plugin Hook. And check the plugin is successfully activated.

### Windows Rockstar Edition
1. Download the latest [release](https://github.com/DekoKiyo/JapaneseCallouts/releases/latest).
2. Find the GTA5 folder. Almost all players' GTA5 folders are below.<br/>`C:\Program Files\Rockstar Games\Grand Theft Auto V`
3. Unzip and drag or extract the folders and files from the .zip into your GTA5 folder.
4. Run the GTA5 with Rage Plugin Hook. And check the plugin is successfully activated.

## Credits & Resources
**DekoKiyo**: Main Developer / Project Leader / Japanese & English Translator<br/>
**Charlie686**: Advice about the new locale system. Especially callout notifications' translation. Thanks!<br/>
**Albo1125**: The original idea and source code for Bank Heist callout and its dialog system came from his [Assorted Callouts](https://github.com/Albo1125/Assorted-Callouts).<br/>
**kagikn**: One of the developer of [ScriptHookV .NET](https://github.com/scripthookvdotnet/scripthookvdotnet) and wrote the fixed program of help popup, notification, and subtitle's character limits and bugs, when its byte is 2 or higher.<br/>

**Libraries**<br/>
[Newtonsoft.Json](https://www.newtonsoft.com/json): Used for manage the json files. Developed by **James Newton-King**<br/>
[RAGENativeUI](https://github.com/Alexguirre/RAGENativeUI): Used for making the UI. Developed by **Alexguirre**<br/>
[INI File Parser](https://github.com/rickyah/ini-parser): Used for parse the ini files. Developed by **rickyah**<br/>
[Costura.Fody](https://github.com/Fody/Costura): Used for embedding the resources. Developed by **Fody Team**<br/>
[IPT.Common](https://github.com/Immersive-Plugins-Team/IPT.Common): Used for check the world weather. Developed by **Immersive Plugins Team**.

## Contributing
The best point of an open-source project is that everyone can join the development. Let's start to develop with us to make a better plugin today!
### How to make the environment of development
- Clone the repository in your computer
1. Fork this repository.
2. Clone your forked repository.
3. Commit your change.
4. Create a pull request from yours.
> [!TIP]
> [Visual Studio Code](https://code.visualstudio.com) is very useful for managing the git repository.<br/>
> Of course, using [Visual Studio](https://visualstudio.microsoft.com) is good too, but VSCode is better than VS for me.
- Create environment variable<br/>
  To configure environment variables on your Windows system for your application, follow these steps:
1. Open the Start Menu and search for 'Edit the system environment variables' and select it.
2. In the System Properties window, click on the 'Environment Variables...' button.
3. Under the 'System variables' section, click on the 'New...' button to create a new environment variable.
4. Add these environment variables below:

|       Name       |               Value                |                              Example                               |
| :--------------: | :--------------------------------: | :----------------------------------------------------------------: |
| GrandTheftAutoV  |       (Your GTA5 Directory)        | `C:\Program Files (x86)\Steam\steamapps\common\Grand Theft Auto V` |
| JapaneseCallouts | (Your Japanese Callouts Directory) |        `C:\Users\%username%\source\repos\JapaneseCallouts`         |

1. Click 'OK' to save the variable, and again 'OK' to close the Environment Variables window.
2. Finally, click 'OK' in the System Properties window to apply the changes.
> [!WARNING]
> Make sure to restart your application or system to ensure the new environment variables take effect.<br/>
> For more detailed instructions, please refer to the official Windows documentation.

## Copyrights
This project is under the [GPL v3](https://github.com/DekoKiyo/JapaneseCallouts?tab=GPL-3.0-1-ov-file) license.<br/>
Copyright 2024 DekoKiyo, All Rights Reserved<br/>

These are all of the copyrights of the libraries I'm using.<br/>

[Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)
```
The MIT License (MIT)

Copyright (c) 2007 James Newton-King

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
[INI File Parser](https://github.com/rickyah/ini-parser/blob/development/LICENSE)
```
The MIT License (MIT)

Copyright (c) 2008 Ricardo Amores Hernández

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
[RAGE Native UI](https://github.com/alexguirre/RAGENativeUI/blob/master/LICENSE.md)
```
MIT License

Copyright (c) 2016-2023 alexguirre

Copyright (c) 2016 Guad

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
[IPT.Common](https://github.com/Immersive-Plugins-Team/IPT.Common/blob/master/LICENSE.md)
```
MIT License

Copyright (c) 2021 Immersive Plugins Team

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```