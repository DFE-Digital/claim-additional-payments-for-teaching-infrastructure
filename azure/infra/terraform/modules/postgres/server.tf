resource "azurerm_postgresql_server" "app_postgres" {
  name                = format("%s-%s", var.app_rg_name, "db")
  location            = var.rg_location
  resource_group_name = var.app_rg_name

  sku_name = "GP_Gen5_2"

  storage_mb                   = 5120
  backup_retention_days        = 35
  geo_redundant_backup_enabled = false
  auto_grow_enabled            = false

  administrator_login          = data.azurerm_key_vault_secret.postgres_user.value
  administrator_login_password = data.azurerm_key_vault_secret.postgres_pw.value
  version                      = "9.6"
  ssl_enforcement_enabled      = true

  #public_network_access_enabled = false
  # ssl_minimal_tls_version_enforced = "TLS1_2"

  threat_detection_policy {
    disabled_alerts      = []
    email_account_admins = false
    email_addresses = [
      "capt-dev@digital.education.gov.uk",
    ]
    enabled        = true
    retention_days = 90
  }

  tags = merge({
    },
    var.common_tags
  )

}

###############


resource "azurerm_postgresql_server" "app_postgres_11" {
  name                = format("%s-%s", var.app_rg_name, "db11")
  location            = var.rg_location
  resource_group_name = var.app_rg_name

  sku_name = "GP_Gen5_2"

  storage_mb                   = 5120
  backup_retention_days        = 35
  geo_redundant_backup_enabled = false
  auto_grow_enabled            = false

  administrator_login          = data.azurerm_key_vault_secret.postgres_user.value
  administrator_login_password = data.azurerm_key_vault_secret.postgres_pw.value
  version                      = "11"
  ssl_enforcement_enabled      = true

  #public_network_access_enabled = false
  # ssl_minimal_tls_version_enforced = "TLS1_2"

  threat_detection_policy {
    disabled_alerts      = []
    email_account_admins = false
    email_addresses = [
      "capt-dev@digital.education.gov.uk",
    ]
    enabled        = true
    retention_days = 90
  }

  tags = merge({
    },
    var.common_tags
  )

}
