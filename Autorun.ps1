$l = Get-Location
$tmp = $env:TEMP
$releasePath = "https://github.com/dragonmost/BackgroundStripper/releases/download/v0.1/BackgroundStripper.exe"

try {
    New-Item -Path ($tmp + "\baker\") -ItemType "directory" -errorAction SilentlyContinue
    Invoke-WebRequest $releasePath -OutFile ($tmp + "\baker\BackgoundStripper.exe")

    Start-Process -FilePath ($tmp + "\baker\BackgoundStripper.exe") -ArgumentList $l.Path -Wait
}
finally {
    Remove-Item ($tmp + "\baker\") -Recurse -Force
}
