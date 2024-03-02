$ReleaseFolder = ".\Release"
$GrandTheftAutoVFolder = ".\Release\GrandTheftAutoV"
$JapaneseCalloutsFolder = ".\Release\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts"
$AudioFolder = ".\Release\GrandTheftAutoV\LSPDFR\Audio\scanner"

$PluginDllFile = ".\JapaneseCallouts\bin\Release\net48\JapaneseCallouts.dll"
$PluginIniFile = ".\JapaneseCallouts\JapaneseCallouts.ini"
$PluginAudioFolder = ".\JapaneseCallouts\Audio"
$PluginScannerAudioFolder = ".\JapaneseCalloutsAudio"

$ZipOutput = "./Release/Japanese Callouts.zip"

If (Test-Path $ReleaseFolder)
{
    Remove-Item $ReleaseFolder -Recurse -Force -Confirm:$false
}

dotnet build -c Release

$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

$PluginsFolder = $GrandTheftAutoV + "\plugins"
$PluginsLSPDFRFolder = $GrandTheftAutoV + "\plugins\LSPDFR"

If (!(Test-Path $PluginsFolder))
{
    New-Item $PluginsFolder -ItemType Directory
}
If (!(Test-Path $PluginsLSPDFRFolder))
{
    New-Item $PluginsLSPDFRFolder -ItemType Directory
}

Copy-Item $PluginDllFile $PluginsLSPDFRFolder
Copy-Item $PluginIniFile $PluginsLSPDFRFolder

New-Item $JapaneseCalloutsFolder -ItemType Directory
Copy-Item $PluginDllFile .\Release\GrandTheftAutoV\plugins\LSPDFR
Copy-Item $PluginIniFile .\Release\GrandTheftAutoV\plugins\LSPDFR
Copy-Item $PluginAudioFolder .\Release\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts\ -Recurse

New-Item $AudioFolder -ItemType Directory
Copy-Item $PluginScannerAudioFolder .\Release\GrandTheftAutoV\LSPDFR\Audio\scanner\ -Recurse

7z.exe a $ZipOutput $GrandTheftAutoVFolder