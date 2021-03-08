data "azurerm_subnet" "subnet_01" {
  #name = format("%s-%s", substr(var.core_rg_name, 0, 7), "sn-01") # <- none-infradev RG's
  name                 = format("%s-%s", substr(var.core_rg_name, 0, 16), "sn-01")
  resource_group_name  = var.core_rg_name
  virtual_network_name = var.core_vn_01_name
}
