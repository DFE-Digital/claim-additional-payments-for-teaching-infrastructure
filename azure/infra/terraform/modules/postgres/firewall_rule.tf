# resource "azurerm_postgresql_firewall_rule" "filewall_exp" {
#   name                = "office"
#   resource_group_name                  = var.app_rg_name
#   server_name                          = azurerm_postgresql_server.app_postgres.name
#   start_ip_address    = "40.112.8.12"
#   end_ip_address      = "40.112.8.12"
# }
