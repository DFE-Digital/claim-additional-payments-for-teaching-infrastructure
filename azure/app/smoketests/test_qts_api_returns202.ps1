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

$response = Invoke-WebRequest -Uri $uri -Method 'GET' -Headers $headers 
#-SkipHeaderValidation -UseBasicParsing

$statusCode = $response.StatusCode
$result = $response | ConvertFrom-Json
 
if ( $statusCode -ne 200 ) {
    throw 'Response is not 200'
}
else {
    $expected = '{       
        "id": 1,
        "trn": "1234567",
        "name": "Test1 Test1",
        "doB": "1956-01-30T00:00:00",
        "niNumber": "SS349378C",
        "qtsAwardDate": "2015-07-04T00:00:00",
        "ittSubject1Code": "G100",
        "ittSubject2Code": "NULL",
        "ittSubject3Code": "NULL",
        "activeAlert": false,
        "qualificationName": "Professional Graduate Certificate in Education",
        "ittStartDate": "2014-08-31T00:00:00"
      }' | ConvertFrom-Json

    $actual = $result.data[0]

    if (Compare-Object $expected.PSObject.Properties $actual.PSObject.Properties) {
        throw 'Actual result doesnot match response'
    }
    else {
        Write-Host 'Received response successfully'
    }
}
