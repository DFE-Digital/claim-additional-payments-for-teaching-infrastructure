resource "azurerm_nat_gateway" "nat_gateway_01" {
  name                = format("%s-%s", var.func_rg_name, "nat-gateway")
  resource_group_name = var.func_rg_name
  location            = var.rg_location
  # public_ip_prefix_ids    = [data.azurerm_public_ip_prefix.pipprefix.id]
  sku_name                = "Standard"
  idle_timeout_in_minutes = 4
  zones                   = []

  tags = merge({
    },
    var.common_tags
  )

}

