dotnet build -c Release

$ReleaseFolder = "./Release"

If (!(test-path $ReleaseFolder))
{
    New-Item $ReleaseFolder -ItemType Directory
}