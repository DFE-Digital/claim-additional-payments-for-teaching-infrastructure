resource "azurerm_application_insights" "app_appinsight" {
  name                = format("%s-%s", var.app_rg_name, "ai")
  location            = var.rg_location
  resource_group_name = var.app_rg_name
  application_type    = "web"
  sampling_percentage = 0

  tags = merge({
    format("%s%s%s%s%s%s%s", "hidden-link:/subscriptions/", data.azurerm_client_config.current.subscription_id, "/resourceGroups/", var.app_rg_name, "/providers/Microsoft.Web/sites/", var.app_rg_name, "-as") = "Resource"
    },
    var.common_tags
  )
}
