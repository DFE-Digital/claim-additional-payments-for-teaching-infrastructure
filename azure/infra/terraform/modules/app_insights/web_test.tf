resource "azurerm_application_insights_web_test" "availability" {
  for_each                = local.webtests
  name                    = each.value.name
  location                = var.rg_location
  resource_group_name     = var.app_rg_name
  application_insights_id = azurerm_application_insights.app_appinsight.id
  kind                    = each.value.kind
  frequency               = each.value.frequency
  timeout                 = each.value.timeout
  enabled                 = each.value.enabled
  retry_enabled           = each.value.retry_enabled
  geo_locations           = each.value.geo_locations

  configuration = <<EOT
<WebTest Name="${each.value.name}" Id="${random_uuid.availability[each.key].result}" Enabled="True" CssProjectStructure="" CssIteration="" Timeout="0" WorkItemIds="" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010" 
Description="" CredentialUserName="" CredentialPassword="" PreAuthenticate="True" Proxy="default" StopOnError="False" RecordedResultFile="" ResultsLocale="">
	<Items>
		<Request Method="GET" Guid="a5f10126-e4cd-570d-961c-cea43999a200" Version="1.1" Url="https://${each.value.URL}" ThinkTime="0" Timeout="300" ParseDependentRequests="True" FollowRedirects="True" RecordResult="True" Cache="False" ResponseTimeGoal="0" Encoding="utf-8" ExpectedHttpStatusCode="200" ExpectedResponseUrl="" ReportingName="" IgnoreHttpStatusCode="False" />
	</Items>
</WebTest>
EOT

  tags = merge({
    "hidden-link:${azurerm_application_insights.app_appinsight.id}" = "Resource"
    #    /subscriptions/", data.azurerm_client_config.current.subscription_id, "/resourceGroups/", var.app_rg_name, "/providers/microsoft.insights/components/", var.app_rg_name, "-ai") = "Resource"
    },
    var.common_tags
  )

}

