$env = $args[0]
Write-Host "`Comtrade.FinanceTrack.User.Api BACKEND script`n" -ForegroundColor Green


$container = docker ps -aq --filter "name=comtrade-financetrack-user-api"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop comtrade-financetrack-user-api
    docker rm comtrade-financetrack-user-api
    "Container removed!"
}

$image = docker images -aq --filter "reference=comtrade-financetrack-user-api-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm comtrade-financetrack-user-api-image
    "Image removed!"
}
$currentPath = Get-Location

".NET publish start"
Set-Location "Api/Comtrade.FinanceTrack.User.Api"
dotnet publish -c Release --output "/bin" --framework net8.0
".NET publish finished"

"Docker build start"
Set-Location $currentPath
docker build -t comtrade-financetrack-user-api-image:latest -f Api/Comtrade.FinanceTrack.User.Api/Dockerfile . --progress=plain
"Docker build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save comtrade-financetrack-user-api-image:latest -o C:\Images\comtrade-financetrack-user-api-image.tar
"Image succesfully saved!" 


"Starting container..."
docker run -dit -p  9102:9102 --restart unless-stopped --name comtrade-financetrack-user-api  comtrade-financetrack-user-api-image







