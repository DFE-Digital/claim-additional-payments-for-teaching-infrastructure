resource "azurerm_route_table" "core_rt_01" {
  name                          = format("%s-%s", var.core_rg_name, "rt-01")
  location                      = var.rg_location
  resource_group_name           = var.core_rg_name
  disable_bgp_route_propagation = false

  route {
    name           = "DFE_LONDON_RAS"
    address_prefix = "192.168.0.0/20"
    next_hop_type  = "VNetLocal"
  }

  route {
    name           = "Internet"
    address_prefix = "0.0.0.0/0"
    next_hop_type  = "Internet"
  }

  tags = merge({
    },
    var.common_tags
  )

}
