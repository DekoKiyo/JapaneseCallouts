# リリース用フォルダたち
$ReleaseFolder = ".\Release"
$GrandTheftAutoVFolder = ".\Release\GrandTheftAutoV"
$JapaneseCalloutsFolder = ".\Release\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts"
$AudioFolder = ".\Release\GrandTheftAutoV\LSPDFR\Audio\scanner"

# コピー元のフォルダ&ファイル
$PluginDllFolder = ".\JapaneseCallouts\bin\Release\net48"
$NAudioCoreDllFile = $PluginDllFolder + "\NAudio.Core.dll"
$CalloutInterfaceAPIDllFile = $PluginDllFolder + "\CalloutInterfaceAPI.dll"
$PluginDllFile = $PluginDllFolder + "\JapaneseCallouts.dll"
$PluginIniFile = ".\JapaneseCallouts\JapaneseCallouts.ini"
$PluginAudioFolder = ".\JapaneseCallouts\Audio"
$PluginScannerAudioFolder = ".\JapaneseCalloutsAudio"

# 圧縮ファイル
$ZipOutput = "./Release/Japanese Callouts.zip"

# 古いリリースフォルダを削除
If (Test-Path $ReleaseFolder)
{
    Remove-Item $ReleaseFolder -Recurse -Force -Confirm:$false
}

# .NETのビルドコマンド
dotnet build -c Release

# 環境変数たち
$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

# ここからは通常ビルドでも行うGTA5へのファイルコピー

# GTA5側のフォルダたち
$PluginsFolder = $GrandTheftAutoV + "\plugins"
$PluginsLSPDFRFolder = $GrandTheftAutoV + "\plugins\LSPDFR"
$LanguageFoler = $PluginsLSPDFRFolder + "\JapaneseCallouts\Languages"

# パスの不足に備えて存在しない場合は作成
If (!(Test-Path $PluginsFolder))
{
    New-Item $PluginsFolder -ItemType Directory
}
If (!(Test-Path $PluginsLSPDFRFolder))
{
    New-Item $PluginsLSPDFRFolder -ItemType Directory
}

# ファイルをコピー
Copy-Item $PluginDllFile $PluginsLSPDFRFolder
Copy-Item $PluginIniFile $PluginsLSPDFRFolder

# ビルドフォルダから言語フォルダのみをコピー
Get-ChildItem -Path $PluginDllFolder -Directory | ForEach-Object {
    $Dest = Join-Path -Path $LanguageFoler -ChildPath $_.Name
    If(Test-Path $Dest)
    {
        Remove-Item $Dest -Recurse -Force -Confirm:$false
    }
    Copy-Item -Path $_.FullName -Destination $Dest -Recurse
}

# GTA5へのファイルコピーここまで

# フォルダ作成
New-Item $JapaneseCalloutsFolder -ItemType Directory
# 圧縮フォルダへのデータコピー
Copy-Item $NAudioCoreDllFile .\Release\GrandTheftAutoV\
Copy-Item $CalloutInterfaceAPIDllFile .\Release\GrandTheftAutoV\
Copy-Item $PluginDllFile .\Release\GrandTheftAutoV\plugins\LSPDFR
Copy-Item $PluginIniFile .\Release\GrandTheftAutoV\plugins\LSPDFR
Copy-Item $PluginAudioFolder .\Release\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts\ -Recurse

# 言語フォルダをコピー
Get-ChildItem -Path $PluginDllFolder -Directory | ForEach-Object {
    $Dest = Join-Path -Path .\Release\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts\Languages -ChildPath $_.Name
    Copy-Item -Path $_.FullName -Destination $Dest -Recurse
}

# 音声ファイル(LSPDFRフォルダ側)をコピー
New-Item $AudioFolder -ItemType Directory
Copy-Item $PluginScannerAudioFolder .\Release\GrandTheftAutoV\LSPDFR\Audio\scanner\ -Recurse

# 7-zipで圧縮
7z.exe a $ZipOutput $GrandTheftAutoVFolder