$env = $args[0]
Write-Host "`Comtrade.FinanceTrack.SyncService BACKEND script`n" -ForegroundColor Green


$container = docker ps -aq --filter "name=comtrade-financetrack-syncservice-increase-worker"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop comtrade-financetrack-syncservice-increase-worker
    docker rm comtrade-financetrack-syncservice-increase-worker
    "Container removed!"
}

$image = docker images -aq --filter "reference=comtrade-financetrack-syncservice-increase-worker-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm comtrade-financetrack-syncservice-increase-worker-image
    "Image removed!"
}
$currentPath = Get-Location

".NET publish start"
Set-Location "WorkerService/Comtrade.FinanceTrack.SyncService.Increase"
dotnet publish -c Release --output "/bin" --framework net8.0
".NET publish finished"

"Docker build start"
Set-Location $currentPath
docker build -t comtrade-financetrack-syncservice-increase-worker-image:latest -f WorkerService/Comtrade.FinanceTrack.SyncService.Increase/Dockerfile . --progress=plain
"Docker build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save comtrade-financetrack-syncservice-increase-worker-image:latest -o C:\Images\comtrade-financetrack-syncservice-increase-worker-image.tar
"Image succesfully saved!" 


"Starting container..."
docker run -dit -p  9103:80 --restart unless-stopped --name comtrade-financetrack-syncservice-increase-worker  comtrade-financetrack-syncservice-increase-worker-image







