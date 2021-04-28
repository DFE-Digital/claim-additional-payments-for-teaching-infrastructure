resource "azurerm_container_registry" "acr" {
  name                = format("%s%s", var.rg_prefix, "contreg")
  location            = var.rg_location
  resource_group_name = var.cont_rg_name
  sku                 = "Standard"
  admin_enabled       = false
  #georeplication_locations = ["East US", "West Europe"]

  tags = merge({
    },
    var.common_tags
  )

}
