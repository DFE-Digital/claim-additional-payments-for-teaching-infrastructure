data "azurerm_public_ip" "pip" {
  name                = format("%s-%s", var.func_rg_name, "pip")
  resource_group_name = var.func_rg_name
}

# data "azurerm_public_ip_prefix" "pipprefix" {
#   name                = format("%s-%s", var.func_rg_name, "pipprefix")
#   resource_group_name = var.func_rg_name
# }

data "azurerm_subnet" "subnet_01" {
  name                 = "default"
  resource_group_name  = format("%s-%s", var.rg_prefix, "ProjectCore")
  virtual_network_name = format("%s-%s", var.rg_prefix, "ProjectCore-wkrvn")
}
