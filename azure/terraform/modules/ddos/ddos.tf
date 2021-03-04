resource "azurerm_network_ddos_protection_plan" "ddos" {
  name                = "s118d01-ddos-example-01"
  location            = var.rg_location
  resource_group_name = var.rg_name

  tags = merge({
    },
    var.common_tags
  )

}
