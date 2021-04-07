[CmdletBinding()]
param ( 
    $SFTPHostName,
    $SFTPUserName,
    $SFTPPassword,
    $SFTPSshHostKeyFingerprint,
    $WinSCPnetDLLPath,
    $SFTPRemotePath,
    $FileToUploadPath,
    $functionappName,
    $apiKey,
    $functionAppKey
)

# Load WinSCP .NET assembly
Add-Type -Path $WinSCPnetDLLPath

# Set up session options
$sessionOptions = New-Object WinSCP.SessionOptions -Property @{
    Protocol              = [WinSCP.Protocol]::Sftp
    HostName              = $SFTPHostName
    UserName              = $SFTPUserName
    Password              = $SFTPPassword
    SshHostKeyFingerprint = $SFTPSshHostKeyFingerprint
}
$session = New-Object WinSCP.Session
try {
    # Connect
    $session.Open($sessionOptions)
    $uploadPath = "/*"
    # Transfer files
    # $session.PutFiles("C:\Users\chandrakasetty\Documents\dqtecptestextract.csv", "/upload/dev/*").Check()
    $session.PutFiles($FileToUploadPath, "$SFTPRemotePath$uploadPath").Check()
}
finally {
    $session.Dispose()
}

Start-Sleep -s 15

$uri = "https://$functionappName.azurewebsites.net/api/qualified-teachers/qualified-teaching-status?code=$functionAppKey&trn=1234567&ni=SS349378C"
 
$headers = @{}
$headers.Add("x-correlation-id", "9B28C533-D8B1-475C-9569-A7C2F60DFC16")
$headers.Add("Authorization", $apiKey)

$response = Invoke-WebRequest -Uri $uri -Method 'GET' -Headers $headers -SkipHeaderValidation -UseBasicParsing

$statusCode = $response.StatusCode
 
if ( $statusCode -ne 200 ) {
    throw 'Response is not 200'
}
else {
    Write-Host 'Received response successfully'
}
