resource "azurerm_log_analytics_workspace" "core-la" {
  name                = format("%s-%s", var.core_rg_name, "la-01")
  location            = var.rg_location
  resource_group_name = var.core_rg_name
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = merge({
    },
    var.common_tags
  )

}
