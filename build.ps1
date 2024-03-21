# 環境変数たち
$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

# GTA5側のディレクトリたち
$PluginsFolder = $GrandTheftAutoV + "\plugins"
$PluginsLSPDFRFolder = $GrandTheftAutoV + "\plugins\LSPDFR"
$LanguageFoler = $PluginsLSPDFRFolder + "\JapaneseCallouts\Languages"

# ビルド側のディレクトリたち
$PluginDllFolder = ".\JapaneseCallouts\bin\Release\net48"
$PluginDllFile = $PluginDllFolder + "\JapaneseCallouts.dll"
$PluginIniFile = ".\JapaneseCallouts\JapaneseCallouts.ini"

# .NETのビルドコマンド
dotnet build -c Release

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