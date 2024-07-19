$env = $args[0]
Write-Host "`Comtrade.FinanceTrack.SyncService BACKEND script`n" -ForegroundColor Green


$container = docker ps -aq --filter "name=comtrade-financetrack-syncservice-decrease-worker"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop comtrade-financetrack-syncservice-decrease-worker
    docker rm comtrade-financetrack-syncservice-decrease-worker
    "Container removed!"
}

$image = docker images -aq --filter "reference=comtrade-financetrack-syncservice-decrease-worker-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm comtrade-financetrack-syncservice-decrease-worker-image
    "Image removed!"
}
$currentPath = Get-Location

".NET publish start"
Set-Location "WorkerService/Comtrade.FinanceTrack.SyncService.Decrease"
dotnet publish -c Release --output "/bin" --framework net8.0
".NET publish finished"

"Docker build start"
Set-Location $currentPath
docker build -t comtrade-financetrack-syncservice-decrease-worker-image:latest -f WorkerService/Comtrade.FinanceTrack.SyncService.Decrease/Dockerfile . --progress=plain
"Docker build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save comtrade-financetrack-syncservice-decrease-worker-image:latest -o C:\Images\comtrade-financetrack-syncservice-decrease-worker-image.tar
"Image succesfully saved!" 


"Starting container..."
docker run -dit -p  9107:80 --restart unless-stopped --name comtrade-financetrack-syncservice-decrease-worker  comtrade-financetrack-syncservice-decrease-worker-image







