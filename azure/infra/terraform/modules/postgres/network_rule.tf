resource "azurerm_postgresql_virtual_network_rule" "projcore_vnet_rule" {
  #  name                                 = format("%s-%s", azurerm_postgresql_server.app_postgres.name, "vnet-rule")
  name                                 = "worker"
  resource_group_name                  = var.app_rg_name
  server_name                          = azurerm_postgresql_server.app_postgres.name
  subnet_id                            = "/subscriptions/8655985a-2f87-44d7-a541-0be9a8c2779d/resourceGroups/s118d01-ProjectCore/providers/Microsoft.Network/virtualNetworks/s118d01-ProjectCore-wkrvn/subnets/worker" # var.projcore_sn_worker_id
  ignore_missing_vnet_service_endpoint = false
}
