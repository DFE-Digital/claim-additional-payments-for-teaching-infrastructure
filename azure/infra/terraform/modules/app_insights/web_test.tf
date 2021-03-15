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


########################

# locals {
#   webtest_01_name = format("%s-%s-%s-%s", var.app_rg_name, "ai-at", var.app_rg_name, "as-webtest")
#   webtest_02_name = format("%s-%s-%s-%s", var.app_rg_name, "ai-at", var.app_rg_name, "vsp-as-webtest")
# }


# resource "azurerm_application_insights_web_test" "av_test_01" {
#   name                    = local.webtest_01_name
#   location                = var.rg_location
#   resource_group_name     = var.app_rg_name
#   application_insights_id = azurerm_application_insights.app_appinsight.id
#   kind                    = "ping"
#   frequency               = 300
#   timeout                 = 120
#   enabled                 = true
#   retry_enabled           = true
#   geo_locations           = ["emea-nl-ams-azr", "emea-ru-msa-edge", "emea-se-sto-edge", "emea-gb-db3-azr", "emea-fr-pra-edge"]

#   configuration = <<EOT
# <WebTest Name="${local.webtest_01_name}" Id="${random_uuid.webtest_01_uuid.result}" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010" Description="" CredentialUserName="" CredentialPassword="" PreAuthenticate="True" Proxy="default" StopOnError="False" RecordedResultFile="" ResultsLocale="">
# 	<Items>
# 		<Request Method="GET" Guid="a5f10126-e4cd-570d-961c-cea43999a200" Version="1.1" Url="https://${local.webtest_01_name}.azurewebsites.net/admin/healthcheck" ThinkTime="0" Timeout="300" ParseDependentRequests="True" FollowRedirects="True" RecordResult="True" Cache="False" ResponseTimeGoal="0" Encoding="utf-8" ExpectedHttpStatusCode="200" ExpectedResponseUrl="" ReportingName="" IgnoreHttpStatusCode="False" />
# 	</Items>
# </WebTest>
# EOT

#   tags = merge({
#     format("%s%s%s%s%s%s%s", "hidden-link:/subscriptions/", data.azurerm_client_config.current.subscription_id, "/resourceGroups/", var.app_rg_name, "/providers/microsoft.insights/components/", var.app_rg_name, "-ai") = "Resource"
#     },
#     var.common_tags
#   )

# }

# resource "azurerm_application_insights_web_test" "av_test_02" {
#   name                    = local.webtest_02_name
#   location                = var.rg_location
#   resource_group_name     = var.app_rg_name
#   application_insights_id = azurerm_application_insights.app_appinsight.id
#   kind                    = "ping"
#   frequency               = 300
#   timeout                 = 120
#   enabled                 = true
#   retry_enabled           = true
#   geo_locations           = ["emea-nl-ams-azr", "emea-ru-msa-edge", "emea-se-sto-edge", "emea-gb-db3-azr", "emea-fr-pra-edge"]

#   configuration = <<EOT
# <WebTest Name="${local.webtest_02_name}" Id="${random_uuid.webtest_02_uuid.result}" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010" Description="" CredentialUserName="" CredentialPassword="" PreAuthenticate="True" Proxy="default" StopOnError="False" RecordedResultFile="" ResultsLocale="">
# 	<Items>
# 		<Request Method="GET" Guid="a5f10126-e4cd-570d-961c-cea43999a200" Version="1.1" Url="https://${local.webtest_02_name}.azurewebsites.net/admin/healthcheck" ThinkTime="0" Timeout="300" ParseDependentRequests="True" FollowRedirects="True" RecordResult="True" Cache="False" ResponseTimeGoal="0" Encoding="utf-8" ExpectedHttpStatusCode="200" ExpectedResponseUrl="" ReportingName="" IgnoreHttpStatusCode="False" />
# 	</Items>
# </WebTest>
# EOT

#   tags = merge({
#     format("%s%s%s%s%s%s%s", "hidden-link:/subscriptions/", data.azurerm_client_config.current.subscription_id, "/resourceGroups/", var.app_rg_name, "/providers/microsoft.insights/components/", var.app_rg_name, "-ai") = "Resource"
#     },
#     var.common_tags
#   )

# }
