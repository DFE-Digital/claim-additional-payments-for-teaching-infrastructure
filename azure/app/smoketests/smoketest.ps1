[CmdletBinding()]
param ( 
    $SFTPHostName,
    $SFTPUserName,
    [SecureString] $SFTPPassword,
    $SFTPSshHostKeyFingerprint,
    $WinSCPnetDLLPath,
    $SFTPRemotePath,
    $FileToUploadPath
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
    # Transfer files
    # $session.PutFiles("C:\Users\chandrakasetty\Documents\dqtecptestextract.csv", "/upload/dev/*").Check()
    $session.PutFiles($FileToUploadPath, $SFTPRemotePath).Check()
}
finally {
    $session.Dispose()
}
 