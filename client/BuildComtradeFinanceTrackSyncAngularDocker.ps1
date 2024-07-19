$env = $args[0]
Write-Host "`nFinanceTrack APP script`n" -ForegroundColor Green



$container = docker ps -aq --filter "name=financetrack-angular-custom"
if(-not [string]::IsNullOrEmpty($container))
{
    "Removing container..."
    docker stop financetrack-angular-custom
    docker rm financetrack-angular-custom
    "Container removed!"
}

$image = docker images -aq --filter "reference=financetrack-angular-image"
if(-not [string]::IsNullOrEmpty($image))
{
    "Removing image..."
    docker image rm financetrack-angular-image
    "Image removed!"
}

#$npmci = Read-Host -Prompt 'Run npm ci (to reinstall node_modules)? (y/N)'

#if($npmci -eq "y")
#{
#    "npm ci --legacy-peer-deps start"
#    npm ci --legacy-peer-deps
#    "npm ci finished"
#}

#"ng build"
#ng build 
#"ng build finished"

"Docker Build start"
docker build -t financetrack-angular-image:latest -f Dockerfile . --progress=plain
"Docker Build finished"
$Folder = 'C:\Images'

if(!(Test-Path -Path $Folder))
{
    New-Item C:\Images -ItemType directory
}

"Saving image to C:\Images..." 
docker save financetrack-angular-image:latest -o C:\Images\financetrack-angular-image.tar
"Image succesfully saved!" 

"Starting container..."
docker run -dit -p 9106:9106 --restart unless-stopped --name financetrack-angular-custom financetrack-angular-image
