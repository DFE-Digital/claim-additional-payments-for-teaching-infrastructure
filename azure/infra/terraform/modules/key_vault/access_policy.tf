# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "secrets_kv_access" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "6642920a-1aab-49bb-9a20-365131195349"
  #object_id = data.azuread_group.tps_del_team.id

  key_permissions = [
    "Get",
    "List",
    "Update",
    "Create",
    "Import",
    "Delete",
    "Recover",
    "Backup",
    "Restore",
    "Decrypt",
    "Encrypt",
    "UnwrapKey",
    "WrapKey",
    "Verify",
    "Sign",
    #    "purge"
  ]

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete",
    "Recover",
    "Backup",
    "Restore"
    #    "purge"
  ]

  certificate_permissions = [
    "Get",
    "List",
    "Update",
    "Create",
    "Import",
    "Delete",
    "Recover",
    "Backup",
    "Restore",
    "ManageContacts",
    "ManageIssuers",
    "GetIssuers",
    "ListIssuers",
    "SetIssuers",
    "DeleteIssuers",
    #    "purge"
  ]

}

# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "secret_kv_access_aas" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  object_id = "a6621090-e704-45ec-b65f-50257f9d4dcd"
  # object_id = data.azuread_application.az_app_serv.id

  secret_permissions = [
    "get"
  ]

  key_permissions = [
  ]

  certificate_permissions = [
  ]
}

# access policies set after deployment 
resource "azurerm_key_vault_access_policy" "secret_kv_access_ado" {
  key_vault_id = azurerm_key_vault.secrets_kv.id

  tenant_id = data.azurerm_client_config.current.tenant_id
  # object_id = data.azurerm_client_config.current.object_id
  # object_id = "dd4c6c81-ef66-4997-ae36-26aa96ce71ee" # <= app id
  # object_id = "f7925adc-d3c2-4ab3-8edf-430b51abbc5b"
  object_id = "8930adb3-ea8f-4448-b784-b151ecfb223b"

  secret_permissions = [
    "Get"
  ]

  key_permissions = [
  ]

  certificate_permissions = [
  ]
}
