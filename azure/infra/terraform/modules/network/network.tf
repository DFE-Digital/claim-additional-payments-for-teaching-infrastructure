resource "azurerm_virtual_network" "projcore_vn_01" {
  name                  = format("%s-%s", var.projcore_rg_name, "wkrvn")
  resource_group_name   = var.projcore_rg_name
  location              = var.rg_location
  vm_protection_enabled = false
  address_space         = local.ip_address_space
  # dns_servers           = local.ip_dns_servers

  tags = merge({
    },
    var.common_tags
  )

}
