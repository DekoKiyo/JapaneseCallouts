$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

$JapaneseCalloutsFolder = "$($GrandTheftAutoV)\plugins\LSPDFR\JapaneseCallouts"
$AudioFolder = "$($GrandTheftAutoV)\LSPDFR\Audio\scanner"
$PluginScannerAudioFolder = ".\JapaneseCalloutsAudio"
$PluginAudioFolder = ".\JapaneseCallouts\Audio"

Write-Host "PowerShell $($PSVersionTable.PSEdition) Version $($PSVersionTable.PSVersion)" -ForegroundColor Cyan
Set-StrictMode -Version 2.0; $ErrorActionPreference = "Stop"; $ConfirmPreference = "None"; trap { Write-Error $_ -ErrorAction Continue; exit 1 }

If (Test-Path "$($JapaneseCalloutsFolder)\Audio")
{
    Write-Host "[Info] The audio folder ($($JapaneseCalloutsFolder)) was found. The folder will be automatically deleted." -ForegroundColor DarkRed
    Remove-Item "$($JapaneseCalloutsFolder)\Audio" -Recurse -Force -Confirm:$false
}
If (Test-Path "$($AudioFolder)\JapaneseCalloutsAudio")
{
    Write-Host "[Info] The audio folder ($($AudioFolder)) was found. The folder will be automatically deleted." -ForegroundColor DarkRed
    Remove-Item "$($AudioFolder)\JapaneseCalloutsAudio" -Recurse -Force -Confirm:$false
}

Write-Host "[Copy] In progress..." -ForegroundColor DarkCyan
Copy-Item $PluginScannerAudioFolder $AudioFolder -Recurse -Force
Copy-Item $PluginAudioFolder $JapaneseCalloutsFolder -Recurse
Write-Host "[Copy] Done!" -ForegroundColor Green

Write-Host "All process was successfully done!" -ForegroundColor Green