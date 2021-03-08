
# resource "azurerm_network_security_group" "core_nsg_01" {
#   name                = format("%s-%s", var.core_rg_name, "nsg-01")
#   resource_group_name = var.core_rg_name
#   location            = var.rg_location

#   # security_rule {
#   #   name                       = "allow_orcsubnet_inbound"
#   #   description                = "Allow inbound traffic inside the subnet"
#   #   priority                   = 200
#   #   direction                  = "Inbound"
#   #   access                     = "Allow"
#   #   protocol                   = "*"
#   #   source_port_range          = "*"
#   #   destination_port_range     = "*"
#   #   source_address_prefix      = data.azurerm_subnet.subnet_01.address_prefix
#   #   destination_address_prefix = data.azurerm_subnet.subnet_01.address_prefix
#   # }

#   # security_rule {
#   #   name                       = "allow_orcsubnet_outbound"
#   #   description                = "Allow outbound traffic inside the subnet"
#   #   priority                   = 200
#   #   direction                  = "Outbound"
#   #   access                     = "Allow"
#   #   protocol                   = "*"
#   #   source_port_range          = "*"
#   #   destination_port_range     = "*"
#   #   source_address_prefix      = data.azurerm_subnet.subnet_01.address_prefix
#   #   destination_address_prefix = data.azurerm_subnet.subnet_01.address_prefix
#   # }

#   tags = merge({
#     },
#     var.common_tags
#   )

# }
