
resource "azurerm_recovery_services_vault" "core_rs_vault" {
  name                = format("%s-%s", var.core_rg_name, "bv-01")
  location            = var.rg_location
  resource_group_name = var.core_rg_name
  sku                 = "Standard"

  soft_delete_enabled = true

  tags = merge({
    },
    var.common_tags
  )

}
