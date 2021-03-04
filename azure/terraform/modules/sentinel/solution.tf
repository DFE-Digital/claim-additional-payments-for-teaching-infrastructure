resource "azurerm_log_analytics_solution" "solution" {
  for_each = {
    for solution in var.solutions :
    solution.product => {
      publisher     = solution.publisher
      solution_name = split("/", solution.product)[1]
    }
    if solution.publisher == "Microsoft"
  }

  solution_name         = each.value.solution_name
  location              = var.rg_location
  resource_group_name   = var.core_rg_name
  workspace_resource_id = var.la_id
  workspace_name        = var.la_name

  plan {
    publisher = each.value.publisher
    product   = each.key
  }

  tags = merge({
    },
    var.common_tags
  )
}

# # further commands needed
# Install-Module AzSentinel -Scope CurrentUser -Force

# The Sentinel module uses the same Azure AD token as AzConnect so can logon using a service principal 
# as such as you use with Terraform using the following command:
# Connect-AzAccount -Credential $Credential -Tenant "xxxxx-xxxxxx-xxxxxxx-xxxxxx-xxxxx" -ServicePrincipal

# After you have authentiated you can use the commands as documented here to do import of 
# hunting/alert rules –> https://github.com/wortell/AZSentinel/tree/master/docs

# Import-AzSentinelAlertRule -WorkspaceName la-opf-utv-weutest -SettingsFile C:\alertsrule.json

# This is an example of implemented Sentinel Analytics/Alert rules. 
# You can also import multiple JSON rules using the following

# Get-Item .\examples\*.json | Import-AzSentinelAlertRule -WorkspaceName
