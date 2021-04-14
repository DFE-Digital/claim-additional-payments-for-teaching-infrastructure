resource "azurerm_virtual_network" "projcore_vn_01" {
  name                  = format("%s-%s", var.projcore_rg_name, "wkrvn")
  resource_group_name   = var.projcore_rg_name
  location              = var.rg_location
  vm_protection_enabled = false
  address_space         = [data.azurerm_key_vault_secret.vnet_range]
  # dns_servers           = data.azurerm_key_vault_secret.vnet_dns_range

  tags = merge({
    },
    var.common_tags
  )

}
