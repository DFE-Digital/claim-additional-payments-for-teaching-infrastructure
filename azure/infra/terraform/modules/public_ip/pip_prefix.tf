resource "azurerm_public_ip_prefix" "pip_prefix_01" {
  name                = format("%s-%s", var.func_rg_name, "pipprefix")
  resource_group_name = var.func_rg_name
  location            = var.rg_location
  prefix_length       = 30
  zones               = ["1"]

  tags = merge({
    },
    var.common_tags
  )
}

