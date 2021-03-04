# resource "azurerm_subnet_network_security_group_association" "nsg_asc_01" {
#   subnet_id                 = data.azurerm_subnet.subnet_01.id
#   network_security_group_id = azurerm_network_security_group.core_nsg_01.id

# }
