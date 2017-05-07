param (
    $PackageId = "Slalom.Stacks.AspNetCore",
    $ApiKey = "4fdba183-fa91-4650-8dae-760bf2c22337"
)


$lower = $PackageId.ToLowerInvariant();

$json = Invoke-WebRequest -Uri "https://api.nuget.org/v3-flatcontainer/$lower/index.json" | ConvertFrom-Json

foreach($version in $json.versions)
{
    if ($version -gt "1.6") {
        Write-Host "Unlisting $PackageId, Ver $version"
        Invoke-Expression "nuget delete $PackageId $version $ApiKey -source https://api.nuget.org/v3/index.json -NonInteractive"
    }
}
