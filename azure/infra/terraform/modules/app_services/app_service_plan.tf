resource "azurerm_service_plan" "app_asp" {
  name                = format("%s-%s", var.app_rg_name, "asp")
  resource_group_name = var.app_rg_name
  location            = var.rg_location
  os_type             = "Linux"
  sku_name = "P1v2"

  tags = var.common_tags
}
