resource "azurerm_postgresql_database" "app_dev_11" {
  name                = var.db_name
  resource_group_name = var.app_rg_name
  server_name         = azurerm_postgresql_server.app_postgres_11.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}
