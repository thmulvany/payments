param(
	[string]$configuration = 'Release',    
	[string]$OS = 'linux',
    [string]$arch = 'x64',
    [string]$version = '1.0.0-rc1-update1',
    [string]$runtime = "coreclr",
	[string]$dockerHost = "10.181.78.45:4243",
	[string]$dockerImgTag = "payment-method-api"
)

Clear

# $date = ([DateTime]::Now.ToString("yyyy-MM-dd_HH-mm-ss"))

# $currentPath = (Get-Location)
# $profilePath = (Get-Item env:\USERPROFILE).value
# $projectName = Split-Path -leaf -path (Get-Location)
# $projectPath = Join-Path -Path $currentPath -ChildPath "src\$projectName"
# $publishPath = Join-Path -Path $profilePath -ChildPath "AppData\Local\Temp\PublishTemp\$projectName"

# write-host "Deploy script started. -- $date --" -foregroundcolor Yellow
# write-host "Configuration: $configuration" -foregroundcolor Yellow
# write-host "Version: $version"  -foregroundcolor Yellow
# write-host "Runtime: $runtime" -foregroundcolor Yellow
# write-host "OS: $OS" -foregroundcolor Yellow
# write-host "Arch: $arch" -foregroundcolor Yellow
# write-host "Docker Host: $dockerHost" -foregroundcolor Yellow
# write-host "Current Path: $currentPath" -foregroundcolor Yellow
# write-host "Profile Path: $profilePath" -foregroundcolor Yellow
# write-host "Project Name: $projectName" -foregroundcolor Yellow
# write-host "Publish Path: $publishPath" -foregroundcolor Yellow

if (-not (test-path env:DOCKER_HOST)) {
    $env:DOCKER_HOST=$dockerHost
}
write-host "docker daemon: $env:DOCKER_HOST"
write-host "stopping and removing running container" -foregroundcolor Yellow
docker stop web
docker rm -v web
write-host "removing image" -foregroundcolor Yellow
docker rmi $dockerImgTag

write-host "wiping publish point and republishing" -foregroundcolor Yellow
if (test-path .\publish) {
    rm -recurse -force .\publish\*
}

dnvm use 1.0.0-rc1-update1 -r coreclr -a x64 -OS win 

push-location ..\src\WebApi\Payments.PaymentMethodApi 
dnu publish . --out ..\..\..\deploy\publish --configuration Release --runtime "dnx-coreclr-linux-x64.1.0.0-rc1-update1" --wwwroot "wwwroot" --wwwroot-out "wwwroot" --quiet
pop-location
#dnu publish $projectPath --out $publishPath --configuration $configuration --runtime "dnx-$runtime-$OS-$arch.$version" --wwwroot "wwwroot" --wwwroot-out "wwwroot" --iis-command "web" --quiet

write-host "building docker image from publish point" -foregroundcolor Yellow
$sw = [Diagnostics.Stopwatch]::StartNew()
docker build -t $dockerImgTag -f .\publish\approot\src\Payments.PaymentMethodApi\Dockerfile .\publish
$sw.Stop()
write-host ("docker build completed in {0:N1} seconds" -f $sw.Elapsed.TotalSeconds) -foregroundcolor Yellow
#docker build -t $dockerImgTag -f (Join-Path -Path $publishPath -ChildPath "approot\src\$projectName\Dockerfile") $publishPath

write-host "creating running instance of docker image" -foregroundcolor Yellow
docker run --name web -t -d -p 80:5000 $dockerImgTag
#docker run --name web -t -d --link local-postgres:pgdb -p 80:5000 $projectName

write-host "tailing logs" -foregroundcolor Yellow
docker logs -f web

return 0;
