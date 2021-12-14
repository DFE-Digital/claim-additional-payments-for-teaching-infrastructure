resource "azurerm_postgresql_virtual_network_rule" "projcore_vnet_rule" {
  #  name                                 = format("%s-%s", azurerm_postgresql_server.app_postgres.name, "vnet-rule")
  name                                 = "worker"
  resource_group_name                  = var.app_rg_name
  server_name                          = azurerm_postgresql_server.app_postgres.name
  subnet_id                            = var.projcore_sn_worker_id
  ignore_missing_vnet_service_endpoint = false
}

###########


resource "azurerm_postgresql_virtual_network_rule" "projcore_vnet_rule_11" {
  #  name                                 = format("%s-%s", azurerm_postgresql_server.app_postgres.name, "vnet-rule")
  name                                 = "worker"
  resource_group_name                  = var.app_rg_name
  server_name                          = azurerm_postgresql_server.app_postgres_11.name
  subnet_id                            = var.projcore_sn_worker_id
  ignore_missing_vnet_service_endpoint = false
}
