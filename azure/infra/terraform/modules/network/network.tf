resource "azurerm_virtual_network" "projcore_vn_01" {
  name                = format("%s-%s", var.projcore_rg_name, "wkrvn")
  resource_group_name = var.projcore_rg_name
  location            = var.rg_location
  # address_space         = ["192.168.1.0/24"]
  # dns_servers           = ["192.168.1.4", "192.168.1.5"]
  vm_protection_enabled = false
  address_space         = ["11.0.1.0/24"]          # <- needs to be
  dns_servers           = ["11.0.1.4", "11.0.1.5"] # <- needs to be

  tags = merge({
    },
    var.common_tags
  )

}
