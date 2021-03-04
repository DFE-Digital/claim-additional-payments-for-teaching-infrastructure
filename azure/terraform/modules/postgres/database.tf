# resource "azurerm_postgresql_database" "app_az_main" {
#   name                = "azure_maintenance"
#   resource_group_name = var.app_rg_name
#   server_name         = azurerm_postgresql_server.app_postgres.name
#   charset             = "UTF8"
#   collation           = "English_United States.1252"
# }

# resource "azurerm_postgresql_database" "app_az_sys" {
#   name                = "azure_sys"
#   resource_group_name = var.app_rg_name
#   server_name         = azurerm_postgresql_server.app_postgres.name
#   charset             = "UTF8"
#   collation           = "English_United States.1252"
# }

resource "azurerm_postgresql_database" "app_dev" {
  name                = "development"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "azurerm_postgresql_database" "dqt_dev" {
  name                = "dqt_poc"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

resource "azurerm_postgresql_database" "dqt" {
  name                = "dqt"
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}
