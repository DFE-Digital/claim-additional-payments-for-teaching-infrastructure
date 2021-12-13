# resource "azurerm_storage_container" "secretssacont_01" {
#   name                  = "insights-logs-auditevent-01"
#   storage_account_name  = azurerm_storage_account.core_secret_01.name
#   container_access_type = "private"

# }

# resource "azurerm_storage_container" "secretstmpsacont_01" {
#   name                  = "insights-logs-auditevent-01"
#   storage_account_name  = azurerm_storage_account.core_secret_tmp_01.name
#   container_access_type = "private"

# }

# resource "azurerm_storage_container" "funcappcont_01" {
#   name                  = "dqt-cont"
#   storage_account_name  = azurerm_storage_account.funcappsa.name
#   container_access_type = "private"

# }
