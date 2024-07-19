$env = $args[0]
Write-Host "`Comtrade.FinanceTrack.CashFlow.Api BACKEND script`n" -ForegroundColor Green


$container = docker ps -aq --filter "name=comtrade-financetrack-cashflow-api"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop comtrade-financetrack-cashflow-api
    docker rm comtrade-financetrack-cashflow-api
    "Container removed!"
}

$image = docker images -aq --filter "reference=comtrade-financetrack-cashflow-api-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm comtrade-financetrack-cashflow-api-image
    "Image removed!"
}
$currentPath = Get-Location

".NET publish start"
Set-Location "Api/Comtrade.FinanceTrack.CashFlow.Api"
dotnet publish -c Release --output "/bin" --framework net8.0
".NET publish finished"

"Docker build start"
Set-Location $currentPath
docker build -t comtrade-financetrack-cashflow-api-image:latest -f Api/Comtrade.FinanceTrack.CashFlow.Api/Dockerfile . --progress=plain
"Docker build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save comtrade-financetrack-cashflow-api-image:latest -o C:\Images\comtrade-financetrack-cashflow-api-image.tar
"Image succesfully saved!" 


"Starting container..."
docker run -dit -p  9101:9101 --restart unless-stopped --name comtrade-financetrack-cashflow-api  comtrade-financetrack-cashflow-api-image







