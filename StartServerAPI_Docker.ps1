Write-Output ""
Set-Location .\Sandwish.Server
Write-Output "Build Server"
dotnet build
if ($LASTEXITCODE -eq 0) {
    Write-Output "Test Server"
    dotnet test
    if ($LASTEXITCODE -eq 0) {
        Write-Output "Publish Server"
        dotnet publish
        if ($LASTEXITCODE -eq 0) {
            Write-Output "Exec Server"
            docker build -t serverapi .
            docker run -p 5001:5001 serverapi
        } else {
            Write-Output "Publish fail"    
        }
    } else {
        Write-Output "Test fail"
    }
} else {
    Write-Output "Build fail"
}

