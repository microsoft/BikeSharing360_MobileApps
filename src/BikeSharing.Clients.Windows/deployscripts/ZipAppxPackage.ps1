Param(
    [string]$dir 
)


function ZipFiles( $zipfilename, $sourcedir )
{
   Add-Type -Assembly System.IO.Compression.FileSystem
   $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
   [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir,
        $zipfilename, $compressionLevel, $false)
}

$folderName = Get-ChildItem "$dir\AppxPackages\" | Select-Object -first 1
Write-Host "creating zip of folder $dir\AppxPackages\"
ZipFiles "$($dir)\$($folderName.Name).zip" "$dir\AppxPackages\";
Write-Host "created zip file with name $($dir)\$($folderName.Name).zip"