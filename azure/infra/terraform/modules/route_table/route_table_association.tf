# resource "azurerm_subnet_route_table_association" "core_sn_rt_assoc_01" {
#   subnet_id      = var.core_sn_id
#   route_table_id = azurerm_route_table.core_rt_01.id
# }
