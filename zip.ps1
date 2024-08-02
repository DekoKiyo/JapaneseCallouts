# リリース用フォルダたち
$ReleaseFolder = ".\Release"
$GrandTheftAutoVFolder = ".\Release\Zip"
$JapaneseCalloutsFolder = ".\Release\Zip\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts"
$AudioFolder = ".\Release\Zip\GrandTheftAutoV\LSPDFR\Audio\scanner"

# コピー元のフォルダ&ファイル
$PluginDllFolder = ".\JapaneseCallouts\bin\Release\net48"
$BaseLibDllFile = $PluginDllFolder + "\BaseLib.dll"
$NAudioCoreDllFile = $PluginDllFolder + "\NAudio.Core.dll"
$CalloutInterfaceAPIDllFile = $PluginDllFolder + "\CalloutInterfaceAPI.dll"
$PluginDllFile = $PluginDllFolder + "\JapaneseCallouts.dll"
$PluginIniFile = ".\JapaneseCallouts\JapaneseCallouts.ini"
$XmlFolder = ".\JapaneseCallouts\Resources\"
$LocalizationFolder = ".\JapaneseCallouts\Localization\"
$ReadmeFolder = ".\Readme\"
$ProjectFile = ".\JapaneseCallouts\JapaneseCallouts.csproj"
$PluginAudioFolder = ".\JapaneseCallouts\Audio"
$PluginScannerAudioFolder = ".\JapaneseCalloutsAudio"

Write-Host "PowerShell $($PSVersionTable.PSEdition) Version $($PSVersionTable.PSVersion)" -ForegroundColor Cyan
Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"; trap { Write-Error $_ -ErrorAction Continue; exit 1 }

function Exec([scriptblock] $cmd) {
    & $cmd
    if ($LASTEXITCODE) { exit $LASTEXITCODE }
}

# If dotnet CLI is installed globally and it matches requested version, use for execution
if ($null -ne (Get-Command "dotnet" -ErrorAction SilentlyContinue) -and `
    $(dotnet --version) -and $LASTEXITCODE -eq 0) {
    $env:DOTNET_EXE = (Get-Command "dotnet").Path
}
else {
    # Download install script
    $DotNetInstallFile = "$TempDirectory\dotnet-install.ps1"
    New-Item -ItemType Directory -Path $TempDirectory -Force | Out-Null
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    (New-Object System.Net.WebClient).DownloadFile($DotNetInstallUrl, $DotNetInstallFile)

    # If global.json exists, load expected version
    if (Test-Path $DotNetGlobalFile) {
        $DotNetGlobal = $(Get-Content $DotNetGlobalFile | Out-String | ConvertFrom-Json)
        if ($DotNetGlobal.PSObject.Properties["sdk"] -and $DotNetGlobal.sdk.PSObject.Properties["version"]) {
            $DotNetVersion = $DotNetGlobal.sdk.version
        }
    }

    # Install by channel or version
    $DotNetDirectory = "$TempDirectory\dotnet-win"
    if (!(Test-Path variable:DotNetVersion)) {
        ExecSafe { & powershell $DotNetInstallFile -InstallDir $DotNetDirectory -Channel $DotNetChannel -NoPath }
    }
    else {
        ExecSafe { & powershell $DotNetInstallFile -InstallDir $DotNetDirectory -Version $DotNetVersion -NoPath }
    }
    $env:DOTNET_EXE = "$DotNetDirectory\dotnet.exe"
}

Write-Host "Microsoft (R) .NET Core SDK Version $(& $env:DOTNET_EXE --version)"

# 古いリリースフォルダを削除
If (Test-Path $ReleaseFolder) {
    Write-Host "The old release was found. It will be deleted." -ForegroundColor Red
    Remove-Item $ReleaseFolder -Recurse -Force -Confirm:$false
}

Write-Host "[Build] In progress..." -ForegroundColor DarkBlue
Exec { & $env:DOTNET_EXE build $ProjectFile -c Release /nodeReuse:false /p:UseSharedCompilation=false -nologo -clp:NoSummary --verbosity quiet }
Write-Host "[Build] Done!" -ForegroundColor Green

$PluginVersion = (Get-Command $PluginDllFile).FileVersionInfo.FileVersion

# 環境変数たち
$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

# ここからは通常ビルドでも行うGTA5へのファイルコピー

# GTA5側のフォルダたち
$PluginsFolder = $GrandTheftAutoV + "\plugins"
$PluginsLSPDFRFolder = $GrandTheftAutoV + "\plugins\LSPDFR"

# パスの不足に備えて存在しない場合は作成
If (!(Test-Path $PluginsFolder)) {
    Write-Host "[Info] The plugins folder ($($PluginsFolder)) was not found. The folder will be automatically generated." -ForegroundColor DarkRed
    New-Item $PluginsFolder -ItemType Directory
}
If (!(Test-Path $PluginsLSPDFRFolder)) {
    Write-Host "[Info] The LSPDFR folder ($($PluginsLSPDFRFolder)) was not found. The folder will be automatically generated." -ForegroundColor DarkRed
    New-Item $PluginsLSPDFRFolder -ItemType Directory
}

# ファイルをコピー
Write-Host "[Copy] In progress..." -ForegroundColor DarkBlue
Copy-Item $PluginDllFile $PluginsLSPDFRFolder
Copy-Item $PluginIniFile $PluginsLSPDFRFolder
try {
    Copy-Item $BaseLibDllFile $GrandTheftAutoV
    Copy-Item $NAudioCoreDllFile $GrandTheftAutoV
    Copy-Item $CalloutInterfaceAPIDllFile $GrandTheftAutoV
}
catch { }
Copy-Item $LocalizationFolder "$($PluginsLSPDFRFolder)\JapaneseCallouts" -Recurse -Force
Write-Host "[Copy] Done!" -ForegroundColor Green

# GTA5へのファイルコピーここまで

Write-Host "[Zip] Start archiving the release files..." -ForegroundColor DarkBlue

# フォルダ作成
Write-Host "[Zip] Create the folder" -ForegroundColor DarkGray
New-Item $JapaneseCalloutsFolder -ItemType Directory
# 圧縮フォルダへのデータコピー
Write-Host "[Zip] Copy the files to archive folder" -ForegroundColor DarkBlue
Copy-Item $BaseLibDllFile .\Release\Zip\GrandTheftAutoV\
Copy-Item $NAudioCoreDllFile .\Release\Zip\GrandTheftAutoV\
Copy-Item $CalloutInterfaceAPIDllFile .\Release\Zip\GrandTheftAutoV\
Copy-Item $PluginDllFile .\Release\Zip\GrandTheftAutoV\plugins\LSPDFR
Copy-Item $PluginIniFile .\Release\Zip\GrandTheftAutoV\plugins\LSPDFR
Copy-Item $XmlFolder .\Release\Zip\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts\ -Recurse
Copy-Item $LocalizationFolder .\Release\Zip\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts\ -Recurse
Copy-Item $ReadmeFolder .\Release\Zip\ -Recurse
Copy-Item $PluginAudioFolder .\Release\Zip\GrandTheftAutoV\plugins\LSPDFR\JapaneseCallouts\ -Recurse

# 音声ファイル(LSPDFRフォルダ側)をコピー
New-Item $AudioFolder -ItemType Directory
Copy-Item $PluginScannerAudioFolder .\Release\Zip\GrandTheftAutoV\LSPDFR\Audio\scanner\ -Recurse


# 圧縮ファイル
$ZipFileName = "Japanese Callouts - $($PluginVersion)"
$ZipOutput = "./Release/$($ZipFileName).zip"

Move-Item $GrandTheftAutoVFolder ".\Release\$($ZipFileName)"

# 7-zipで圧縮
Write-Host "[Zip] Archiving now..." -ForegroundColor DarkMagenta
7z.exe a $ZipOutput ".\Release\$($ZipFileName)"
Write-Host "[Zip] Done!"

Write-Host "Plugin Version is $($PluginVersion)" -ForegroundColor Magenta
Write-Host "All process was successfully done!" -ForegroundColor Green