
param(
    [Parameter(Mandatory=$true)] 
    [String] 
    $subscriptionId, 
    [Parameter(Mandatory=$true)] 
    [String] 
    $ResourceGroupName, 
    [Parameter(Mandatory=$true)] 
    [String]
    $SecretId,  
    [Parameter(Mandatory=$true)] 
    [String]
    $Database, 
    [Parameter(Mandatory=$true)] 
    [String]
    $Server, 
    [parameter(Mandatory=$false,
    ParameterSetName="QueryFile")]
    [String[]]$QueryFile, 
    [parameter(Mandatory=$false,
    ParameterSetName="QueryString")]
    [String[]]$QueryString 
)
try{


    # $AzCred = Get-Credential -UserName 's118d.bsvc.cip.azdo'
    # az login --service-principal -u $AzCred.UserName -p $AzCred.GetNetworkCredential().Password --tenant '9c7d9dd3-840c-4b3f-818e-552865082e16'

    # Change the subscription to the target sub
    az account set --subscription $subscriptionId

    # Get the build server current IP
    $CurrentIP = (Invoke-WebRequest http://ipv4.icanhazip.com).Content.Replace("`n","")
}
catch
{
    exit 1
}
try
{

    # Add a temporary firewall rule to allow the build server connectivity to the SQL Server
    $Rule = az postgres server firewall-rule create -g $ResourceGroupName -s $Server -n TEMPDEPLOY --start-ip-address $CurrentIP --end-ip-address $CurrentIP -o json
    $Rule = $Rule | ConvertFrom-Json
    Write-Output "Rule $($Rule.name) for IP range $($Rule.startIPAddress) - $($Rule.endIPAddress) added to $($Rule.id.split("/")[8])"

    Write-Host "Server is [$($Server)]"+
    $PostGresServer = "$($Server).postgres.database.azure.com"
    $User = "tps_development@$($Server)"
    $Password = az keyvault secret show --id $SecretId --query "value" --output tsv

    Write-Host "Connected to server [$($Server)]"

    $Query = $QueryString 

    if ($null -ne $QueryFile) {
        # Get the SQL script
        Write-Host Attempting Execution of $QueryFile
        $Query = Get-Content $QueryFile
    }

    $psqlArguments = "host=$PostGresServer port=5432 dbname=$Database user=$User password=$Password sslmode=require"

    psql $psqlArguments -c "$Query"

    Write-Host "Query Complete"

}

finally {
    # Remove temporary firewall rule
    Write-Output "Rule TEMPDEPLOY will be removed"

    az postgres server firewall-rule delete --name TEMPDEPLOY --resource-group $ResourceGroupName --server-name $Server --yes -y

    Write-Output "Rule removed"
}
