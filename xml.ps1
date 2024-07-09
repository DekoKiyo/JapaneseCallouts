# 環境変数たち
$GrandTheftAutoV = $env:GrandTheftAutoV

# GTA5側のディレクトリたち
$PluginsLSPDFRFolder = $GrandTheftAutoV + "\plugins\LSPDFR"
$XmlFolder = ".\Xml\"

Write-Host "PowerShell $($PSVersionTable.PSEdition) Version $($PSVersionTable.PSVersion)" -ForegroundColor Cyan
Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"; trap { Write-Error $_ -ErrorAction Continue; exit 1 }

Write-Host "[Copy] In progress..." -ForegroundColor DarkCyan
Copy-Item $XmlFolder "$($PluginsLSPDFRFolder)\JapaneseCallouts" -Recurse -Force
Write-Host "[Copy] Done!" -ForegroundColor Green

Write-Host "All process was successfully done!" -ForegroundColor Green