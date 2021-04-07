resource "azurerm_network_ddos_protection_plan" "ddos" {
  name                = format("%s-%s", var.rg_prefix, "ddos-example-01")
  location            = var.rg_location
  resource_group_name = var.rg_name

  tags = merge({
    },
    var.common_tags
  )

}
