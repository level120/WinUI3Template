using namespace System.Management.Automation
using namespace System.Management.Automation.Language

# Set and start timer

[System.Diagnostics.Stopwatch] $Timer = [System.Diagnostics.Stopwatch]::StartNew()

# Set global format culture

if ($host.Name -eq 'ConsoleHost')
{
    Import-Module PSReadLine
}

[Threading.Thread]::CurrentThread.CurrentCulture = 'en-US'


# Build OS Checking

Write-Host "Build machine os: " -ForegroundColor DarkBlue -NoNewline
Write-Host "$([System.Environment]::OSVersion.VersionString)" -ForegroundColor Yellow


# Build Tools Checking

if (Get-Command -Name "dotnet" -errorAction SilentlyContinue) {
  Write-Host "Detect 'dotnet' command: " -ForegroundColor DarkBlue -NoNewline

  $sdkVersions = (& dotnet --list-sdks).Split([System.Environment]::NewLine) | ForEach-Object -Process {
      New-Object System.Version($_.Split()[0].Split('-')[0])
  }

  $supportSdks = $sdkVersions | Group-Object -Propert Major, Minor | ForEach-Object -Process {
      Join-String -InputObject $_.Group.Major[0], $_.Group.Minor[0] -Separator "."
  } | Join-String -Separator ','

  Write-Host $supportSdks -ForegroundColor Yellow
}


# Set Build Paths

[string] $RootPath = $PSScriptRoot
[string] $Configuration = "Release"
[string] $Platform = "x64"
[string] $Runtime = "win-$Platform"
[string] $Profile = $Runtime
[string] $PublishDir = "$RootPath/src/Atelier/bin/$Profile/publish"


# Clean-up

$Timer.Stop()

Remove-Item -Path $PublishDir -Force -Recurse -Confirm:$false

Write-Host "Clean-up, Working time: " -ForegroundColor DarkBlue -NoNewline
Write-Host "$Timer" -ForegroundColor Yellow


# Build

$Timer.Restart()

& dotnet publish -c $Configuration -r $Runtime /p:Platform=$Platform /p:PublishProfile=$Profile

$Timer.Stop()


# Check Build Result

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build " -ForegroundColor DarkBlue -NoNewline
    Write-Host "failed" -ForegroundColor Red -NoNewline
    Write-Host ", Working time: " -ForegroundColor DarkBlue -NoNewline
    Write-Host "$Timer" -ForegroundColor Yellow
    exit $LASTEXITCODE
}

Write-Host "Build " -ForegroundColor DarkBlue -NoNewline
Write-Host "succeeded" -ForegroundColor Green -NoNewline
Write-Host ", Working time: " -ForegroundColor DarkBlue -NoNewline
Write-Host "$Timer" -ForegroundColor Yellow


# Remove unnessary files

$Timer.Restart()

$files = Get-ChildItem -Path $PublishDir -Recurse -Include *.pdb, Microsoft.UI.Xaml, WindowsAppRuntime.png

Remove-Item -Path $files -Force -Recurse -Confirm:$false

$Timer.Stop()

Write-Host "Remove debug files, Working time: " -ForegroundColor DarkBlue -NoNewline
Write-Host "$Timer" -ForegroundColor Yellow
$files | Write-Host

# Open Publish Directory

Invoke-item -Path $PublishDir
