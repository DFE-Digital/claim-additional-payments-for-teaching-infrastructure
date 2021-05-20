resource "azurerm_security_center_workspace" "sec_centre" {
  scope        = lower(format("%s%s", "/subscriptions/", data.azurerm_client_config.current.subscription_id))
  workspace_id = var.la_id
}
