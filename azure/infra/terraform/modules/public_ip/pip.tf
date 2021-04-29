resource "azurerm_public_ip" "pip_01" {
  name                = format("%s-%s", var.func_rg_name, "pip")
  resource_group_name = var.func_rg_name
  location            = var.rg_location
  allocation_method   = "Static"
  sku                 = "Standard"
  zones               = ["1"]

  tags = merge({
    },
    var.common_tags
  )
}

