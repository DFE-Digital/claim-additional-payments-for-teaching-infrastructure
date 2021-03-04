resource "azurerm_virtual_network" "core_vn_01" {
  name                  = format("%s-%s", var.core_rg_name, "vn-01")
  resource_group_name   = var.core_rg_name
  location              = var.rg_location
  address_space         = ["192.168.18.0/24"]
  dns_servers           = ["192.168.18.4", "192.168.18.5"]
  vm_protection_enabled = false

  tags = merge({
    },
    var.common_tags
  )

}

resource "azurerm_virtual_network" "projcore_vn_01" {
  name                  = format("%s-%s", var.projcore_rg_name, "wkrvn")
  resource_group_name   = var.projcore_rg_name
  location              = var.rg_location
  address_space         = ["192.168.1.0/24"]
  dns_servers           = ["192.168.1.4", "192.168.1.5"]
  vm_protection_enabled = false


  tags = merge({
    },
    var.common_tags
  )

}
