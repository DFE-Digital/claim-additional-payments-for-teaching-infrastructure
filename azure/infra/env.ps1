# These relate to the location of the key vault holding the informationi
$AzureTenantId = "9c7d9dd3-840c-4b3f-818e-552865082e16"
$AzureSubscriptionId = "8655985a-2f87-44d7-a541-0be9a8c2779d"
# $backendrg = "s118d01-infradev"
# $backendsa = "s118d01tfbackend"
$backendrg = "s118d01-tfbackend"
$backendsa = "s118d01tfbackendsa"

# You can set TF_LOG to one of the log levels TRACE, DEBUG, INFO, WARN or ERROR to change the verbosity of the logs. 
# TRACE is the most verbose and it is the default if TF_LOG is set to something other than a log level name.
$LoggingLevel = $null
[System.Environment]::SetEnvironmentVariable("TF_LOG", $LoggingLevel, "Process")

# Log into Azure CLI
$connection = Connect-AzAccount -Subscription $AzureSubscriptionId -Tenant $AzureTenantId

$storageaccount = Get-AzStorageAccount -ResourceGroupName $backendrg -Name $backendsa

$storageaccountkey = (Get-AzStorageAccountKey -ResourceGroupName $backendrg -Name $backendsa)[0].value 

# Write the variables
[System.Environment]::SetEnvironmentVariable("ARM_TENANT_ID", $AzureTenantId, "Process")
[System.Environment]::SetEnvironmentVariable("ARM_SUBSCRIPTION_ID", $AzureSubscriptionId, "Process")
[System.Environment]::SetEnvironmentVariable("ARM_ACCESS_KEY", $storageaccountkey, "Process")
