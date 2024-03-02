$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

$PluginsFolder = $GrandTheftAutoV + "\plugins"
$PluginsLSPDFRFolder = $GrandTheftAutoV + "\plugins\LSPDFR"

$PluginDllFile = ".\JapaneseCallouts\bin\Release\net48\JapaneseCallouts.dll"
$PluginIniFile = ".\JapaneseCallouts\JapaneseCallouts.ini"

dotnet build -c Release

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