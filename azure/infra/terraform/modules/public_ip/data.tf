data "azurerm_subnet" "subnet_01" {
  name                 = "default"
  resource_group_name  = format("%s-%s", var.rg_prefix, "ProjectCore")
  virtual_network_name = format("%s-%s", var.rg_prefix, "ProjectCore-wkrvn")
}

data "azurerm_nat_gateway" "nat_gateway_01" {
  name                = format("%s-%s", var.rg_prefix, "nat-gateway")
  resource_group_name = var.func_rg_name
}
