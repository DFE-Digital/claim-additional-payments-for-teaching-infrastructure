resource "azurerm_public_ip" "pip_01" {
  name                = format("%s-%s", local.pip_name_prefix, "pip")
  resource_group_name = var.func_rg_name
  location            = var.rg_location
  allocation_method   = "Static"
  sku                 = "Standard"
  zones               = []

  tags = merge({
    },
    var.common_tags
  )
}

# s118d-funcapp-pip
# s118t01-funcapp-pip
# s118d-funcapp-pip
