$GrandTheftAutoV = $env:GrandTheftAutoV
$JapaneseCallouts = $env:JapaneseCallouts

$JapaneseCalloutsFolder = "$($GrandTheftAutoV)\plugins\LSPDFR\JapaneseCallouts"
$AudioFolder = "$($GrandTheftAutoV)\LSPDFR\Audio\scanner"
$PluginScannerAudioFolder = ".\JapaneseCalloutsAudio"
$PluginAudioFolder = ".\JapaneseCallouts\Audio"

If (Test-Path "$($JapaneseCalloutsFolder)\Audio")
{
    Remove-Item "$($JapaneseCalloutsFolder)\Audio" -Recurse -Force -Confirm:$false
}
If (Test-Path "$($AudioFolde)\JapaneseCalloutsAudio")
{
    Remove-Item "$($AudioFolde)\JapaneseCalloutsAudio" -Recurse -Force -Confirm:$false
}

# New-Item $AudioFolder -ItemType Directory
# New-Item $JapaneseCalloutsFolder -ItemType Directory
Copy-Item $PluginScannerAudioFolder $AudioFolder -Recurse -Force
Copy-Item $PluginAudioFolder $JapaneseCalloutsFolder -Recurse