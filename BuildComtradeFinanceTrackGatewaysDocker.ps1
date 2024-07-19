$env = $args[0]
Write-Host "`Comtrade.FinanceTrack.Gateways BACKEND script`n" -ForegroundColor Green


$container = docker ps -aq --filter "name=comtrade-financetrack-gateways"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop comtrade-financetrack-gateways
    docker rm comtrade-financetrack-gateways
    "Container removed!"
}

$image = docker images -aq --filter "reference=comtrade-financetrack-gateways-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm comtrade-financetrack-gateways-image
    "Image removed!"
}
$currentPath = Get-Location

".NET publish start"
Set-Location "Gateways/Comtrade.FinanceTrack.Gateways.Default"
dotnet publish -c Release --output "/bin" --framework net8.0
".NET publish finished"

"Docker build start"
Set-Location $currentPath
docker build -t comtrade-financetrack-gateways-image:latest -f Gateways/Comtrade.FinanceTrack.Gateways.Default/Dockerfile . --progress=plain
"Docker build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save comtrade-financetrack-gateways-image:latest -o C:\Images\comtrade-financetrack-gateways-image.tar
"Image succesfully saved!" 


"Starting container..."
docker run -dit -p  9105:9105 --restart unless-stopped --name comtrade-financetrack-gateways  comtrade-financetrack-gateways-image







