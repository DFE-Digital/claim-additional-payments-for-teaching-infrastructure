# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "core_kv_access" {
  key_vault_id = azurerm_key_vault.core_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "6642920a-1aab-49bb-9a20-365131195349" #data.azuread_group.tps_del_team.id 

  key_permissions = [
    "get",
    "list",
    "update",
    "create",
    "import",
    "delete",
    "recover",
    "backup",
    "restore",
    "decrypt",
    "encrypt",
    "unwrapKey",
    "wrapKey",
    "verify",
    "sign",
    "purge"
  ]

  secret_permissions = [
    "get",
    "list",
    "set",
    "delete",
    "recover",
    "backup",
    "restore",
    "purge"
  ]

  certificate_permissions = [
    "get",
    "list",
    "update",
    "create",
    "import",
    "delete",
    "recover",
    "backup",
    "restore",
    "managecontacts",
    "manageissuers",
    "getissuers",
    "listissuers",
    "setissuers",
    "deleteissuers",
    "purge"
  ]

}

# backup management service
# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "core_kv_access_bms" {
  key_vault_id = azurerm_key_vault.core_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "22de2add-1ba5-4805-ad5a-ddbed57f888d" #data.azuread_application.bkp_mngt_serv.id 

  key_permissions = [
    "get",
    "list",
    "backup"
  ]

  secret_permissions = [
    "get",
    "list",
    "backup"
  ]

  certificate_permissions = [
  ]
}

# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "core_kv_access_sat" {
  key_vault_id = azurerm_key_vault.core_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "e93a2021-bfa1-4c80-86df-cd8085f068ab" #data.azuread_application.sec_aud_tool.id 

  key_permissions = [
    "get",
    "list"
  ]

  secret_permissions = [
    "get",
    "list"
  ]

  certificate_permissions = [
    "get",
    "list"
  ]
}

# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "secrets_kv_access" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "6642920a-1aab-49bb-9a20-365131195349" # data.azuread_group.tps_del_team.id 

  key_permissions = [
    "get",
    "list",
    "update",
    "create",
    "import",
    "delete",
    "recover",
    "backup",
    "restore",
    "decrypt",
    "encrypt",
    "unwrapKey",
    "wrapKey",
    "verify",
    "sign"
    #    "purge"
  ]

  secret_permissions = [
    "get",
    "list",
    "set",
    "delete",
    "recover",
    "backup",
    "restore"
    #    "purge"
  ]

  certificate_permissions = [
    "get",
    "list",
    "update",
    "create",
    "import",
    "delete",
    "recover",
    "backup",
    "restore",
    "managecontacts",
    "manageissuers",
    "getissuers",
    "listissuers",
    "setissuers",
    "deleteissuers"
    #    "purge"
  ]

}

# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "secret_kv_access_aas" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "a6621090-e704-45ec-b65f-50257f9d4dcd" #data.azuread_application.az_app_serv.id 

  secret_permissions = [
    "get"
  ]

  key_permissions = [
  ]

  certificate_permissions = [
  ]
}
