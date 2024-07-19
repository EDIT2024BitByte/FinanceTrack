$env = $args[0]
Write-Host "`Comtrade.FinanceTrack.Budget.Api BACKEND script`n" -ForegroundColor Green


$container = docker ps -aq --filter "name=comtrade-financetrack-budget-api"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop comtrade-financetrack-budget-api
    docker rm comtrade-financetrack-budget-api
    "Container removed!"
}

$image = docker images -aq --filter "reference=comtrade-financetrack-budget-api-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm comtrade-financetrack-budget-api-image
    "Image removed!"
}
$currentPath = Get-Location

".NET publish start"
Set-Location "Api/Comtrade.FinanceTrack.Budget.Api"
dotnet publish -c Release --output "/bin" --framework net8.0
".NET publish finished"

"Docker build start"
Set-Location $currentPath
docker build -t comtrade-financetrack-budget-api-image:latest -f Api/Comtrade.FinanceTrack.Budget.Api/Dockerfile . --progress=plain
"Docker build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save comtrade-financetrack-budget-api-image:latest -o C:\Images\comtrade-financetrack-budget-api-image.tar
"Image succesfully saved!" 


"Starting container..."
docker run -dit -p  9100:9100 --restart unless-stopped --name comtrade-financetrack-budget-api  comtrade-financetrack-budget-api-image







